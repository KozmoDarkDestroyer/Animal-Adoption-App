using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using animal_adoption.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
 
            if (user.UserName == "johndoe" && user.Password == "def@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
 
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
 
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Manager")
                };
 
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5001",
                audience: "http://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );
 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { token = tokenString });
            }
        else
        {
            return Unauthorized();
        }
        }
    }
}