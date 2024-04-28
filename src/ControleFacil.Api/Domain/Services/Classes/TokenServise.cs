using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControleFacil.Api.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class TokenServise
    {
        private readonly IConfiguration _configuration;

        public TokenServise(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken (Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["KeySecret"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),

                }),

                Expires = DateTime.UtcNow.AddHours(Convert.ToUInt32(_configuration["HorasValidadeToken"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
        
    }
}