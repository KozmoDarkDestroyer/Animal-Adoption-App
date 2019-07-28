using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Functions;
using animal_adoption.Models;
using animal_adoption.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public authController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<User>> Login ([FromBody] Login model){
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            User user = await db.User
                .Where(k => k.email == model.email)
                .FirstOrDefaultAsync();
        
            if (user == null)
            {
                return NotFound(new {
                    ok = false,
                    message = "The username or password is incorrect"
                });
            }

            if (Encrypt.Decrypt(user.password) != model.password)
            {
                return Unauthorized(new {
                    ok = false,
                    message = "The username or password is incorrect"
                });
            }
 
            if (user.role == "ADMIN")
            {
                string tokenString = generateToken(user,"ADMIN");
                return Ok(new { token = tokenString });
            }

            else if(user.role == "USER"){
                string tokenString = generateToken(user,"USER");
                return Ok(new { token = tokenString }); 
            }
            else{
                return BadRequest(new {
                    ok = false,
                    err = "Valid user roles are USER or ADMIN"
                });
            }
            
        }

        private string generateToken (User user, string role){
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("V5auRHrgNThK31fYLnOvyMj0sCmkBXzZ"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Email, user.email)
        };
            
        var tokeOptions = new JwtSecurityToken(
            issuer: "http://localhost:5001",
            audience: "http://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
        );
 
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
    
    }

}