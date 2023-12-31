using User_management.API.Models;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using User_management.API.Services;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UsersContext>(options =>
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
});
builder.Services.AddScoped<UsersContext>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = TokenService.GetTokenValidationParameters();

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                var username = context.Principal.Identity.Name;

                if (!userService.UsernameExists(username))
                    context.Fail("user not found in database");
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireOwnerRole", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Owner");
    });
    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin", "Owner");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Users management API",
        Description = "An ASP.NET Core Web API for managing users accounts and their role",
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("swagger");
        return Task.CompletedTask;
    });
    endpoints.MapControllers();
});

app.Run();
