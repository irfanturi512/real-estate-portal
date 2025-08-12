using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApi.Helpers
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config) => _config = config;

        public string GenerateToken(User user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwt.GetValue<string>("Issuer"),
                audience: jwt.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.GetValue<int>("ExpireMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
