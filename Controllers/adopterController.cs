using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Functions;
using animal_adoption.Models;
using animal_adoption.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adopterController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public adopterController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]"),Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult<Adopter>> Create ([FromBody] AdopterPost model){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Adopter adopter = new Adopter();
            await db.Adopter.AddAsync(AssignsControllers.AssingAdopter(model,adopter));
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
                adopter
            });
        }

        [HttpPut("[action]/{id}"),Authorize(Roles = "ADMIN,USER")]

        public async Task<ActionResult<Adopter>> Update ([FromBody] AdopterPost model, int id){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Adopter adopter = await db.Adopter
                              .Where(k => k.id_adopter == id)
                              .FirstOrDefaultAsync();

            if (adopter == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }

            AssignsControllers.AssingAdopter(model,adopter);
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
                adopter
            });
        }

        [HttpGet("[action]/{id}"),Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult<Adopter>> Get (int id){
            Adopter adopter = await db.Adopter
                              .Include(k => k.Pet)
                              .Include(k => k.Form)
                              .Where(k => k.id_adopter == id)
                              .FirstOrDefaultAsync();
            
            if (adopter == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }

            return Ok(new {
                ok = true,
                adopter
            });
        }

        [HttpGet("[action]"),Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult<Adopter>> List(){
            List<Adopter> adopters = await db.Adopter
                                    .Include(k => k.Pet)
                                    .Include(k => k.Form)
                                    .ToListAsync();

            if (adopters.Count == 0)
            {
                return NotFound(new {
                    ok = false,
                    err = "There are no records in the database"
                });
            }
            return Ok(new {
                ok = true,
                adopters
            });
        }

        [HttpDelete("[action]/{id}"),Authorize(Roles = "ADMIN")]

        public async Task<ActionResult<Adopter>> Delete (int id){
            Adopter adopter = await db.Adopter
                              .Where(k => k.id_adopter == id)
                              .FirstOrDefaultAsync();
            if (adopter == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "There are no records in the database"
                });
            }

            db.Adopter.Remove(adopter);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (System.Exception err)
            {
                return BadRequest(new {
                    ok = true,
                    err = new {
                        message = err.InnerException.Message
                    }
                });
            }

            return Ok(new {
                ok = true,
                adopter
            });
        }
    }
}