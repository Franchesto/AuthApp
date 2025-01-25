using AuthAppCore.Models;
using AuthAppInfrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AuthAppInfrastructure.Services
{
    public sealed class TokenProviderService : ITokenProviderService
    {
        private readonly IConfiguration _configuration;

        public TokenProviderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            string secretKey = _configuration["Jwt:Secret"];

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenPayload = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]          
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenPayload);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}
