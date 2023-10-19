using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using User_management.API.Models;

namespace User_management.API.Services
{
    public class TokenService
    {
        private readonly UsersContext _userContext;

    public TokenService(UsersContext userContext)
        {
            _userContext = userContext;
        }

        public static string GenerateTokenJwt(string username, Role userRole)
        {
            string secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            string audienceToken = Environment.GetEnvironmentVariable("JWT_AUDIENCE_TOKEN");
            string issuerToken = Environment.GetEnvironmentVariable("JWT_ISSUER_TOKEN");
            var expireTime = DateTime.Now.AddMinutes(60);

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, userRole.ToString())
            }); ;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.Now,
                expires: expireTime,
                signingCredentials: signingCredentials);
            
            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            string secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER_TOKEN"),
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE_TOKEN"),
                IssuerSigningKey = securityKey
            };

            return tokenValidationParameters;
        }

    }
}
