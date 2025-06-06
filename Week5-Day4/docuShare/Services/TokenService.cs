using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using docuShare.Interfaces;
using docuShare.Models;
using Microsoft.IdentityModel.Tokens;


namespace docuShare.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _securityKey;
        public TokenService(IConfiguration configuration)
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keys:JwtTokenKey"]));
        }
        public async Task<string> GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty),
                new Claim("AccessLevel", user.Role?.AccessLevel.ToString() ?? "0", ClaimValueTypes.Integer32)
            };
            var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}