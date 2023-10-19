using User_management.API.Models;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using User_management.API.Services;
using System.Security.Claims;

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = TokenService.GetTokenValidationParameters();
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
