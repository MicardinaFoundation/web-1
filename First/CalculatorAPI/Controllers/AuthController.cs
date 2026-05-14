using CalculatorAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public AuthResponse Index([FromBody] AuthRequest model)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Login) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt).ToString()
            };
        }
    }
}
