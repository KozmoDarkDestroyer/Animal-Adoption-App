using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Functions;
using animal_adoption.Models;
using animal_adoption.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public userController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<User>> Create ([FromBody] UserPost model){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            User user = new User();
            await db.AddAsync(AssignsControllers.AssingUser(model,user,"POST"));

            try
            {
                await db.SaveChangesAsync();   
            }
            catch (System.Exception err)
            {
                return BadRequest(new {
                    ok = false,
                    err = new {
                        message = err.InnerException.Message
                    }
                });
            }
            return Ok(new {
                ok = true,
                user = new {
                    user.name,
                    user.email
                }
            });
        }

        [HttpPut("[action]/{id}")]

        public async Task<ActionResult<User>> Update ([FromBody] UserPost model, int id){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            User user = await db.User
                        .Where(k => k.id_user == id)
                        .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
        
            AssignsControllers.AssingUser(model,user,"PUT");

            try
            {
                await db.SaveChangesAsync();   
            }
            catch (System.Exception err)
            {
                return BadRequest(new {
                    ok = false,
                    err = err.InnerException.Message
                });
            }
            return Ok(new {
                ok = true,
                user = new {
                    user.name,
                    user.role,
                    user.status
                }
            });
        }
        
        [HttpGet("[action]/{id}")]

        public async Task<ActionResult<User>> Get (int id){
            User user = await db.User
                        .Where(k => k.id_user == id)
                        .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
            return Ok(new {
                ok = true,
                user
            });
        }

        [HttpGet("[action]")]

        public async Task<ActionResult<User>> List (){
            List<User> users = await db.User.ToListAsync();
            if (users == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "There are no records in the database"
                });
            }
            return Ok(new {
                ok = true,
                users
            });
        }

        [HttpDelete("[action]/{id}")]

        public async Task<ActionResult<User>> Delete(int id){
            User user = await db.User
                        .Where(k => k.id_user == id)
                        .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });  
            }

            user.status = false;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (System.Exception err)
            {
                return BadRequest(new {
                    ok = false,
                    err = new {
                        message = err.InnerException.Message
                    }
                });
            }
            return Ok(new {
                ok = true,
                user
            });
        }
    }
}