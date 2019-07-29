using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Functions;
using animal_adoption.Models;
using animal_adoption.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class petController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public petController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<Pet>> Create([FromBody] PetPost model){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Pet pet = new Pet();
            await db.Pet.AddAsync(AssignsControllers.AssingPet(model,pet));
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
                pet
            });
        }

        [HttpGet("[action]")]

        public async Task<ActionResult<Pet>> List (){
            List<Pet> pets = await db.Pet
                            .Include(k => k.Foundation)
                            .ToListAsync();
            if (pets == null)
            {
                return NotFound(new {
                    ok = false,
                    message = ""
                });
            }
            return Ok(new {
                ok = true,
                pets
            });
        }

        [HttpGet("[action]/{id}")]

        public async Task<ActionResult<Pet>> Get (int id){
            Pet pet = await db.Pet
                     .Include(k => k.Foundation)
                     .Where(k => k.id_pet == id)
                     .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                }); 
            }

            return Ok(new {
                ok = true,
                pet
            });
        }

        [HttpPut("[action]/{id}")]

        public async Task<ActionResult<Pet>> Update ([FromBody] PetPost model, int id){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            Pet pet = await db.Pet
                      .Where(k => k.id_pet == id)
                      .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });
            }
            
            AssignsControllers.AssingPet(model,pet);

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
                pet
            });
        }

        [HttpDelete("[action]")]

        public async Task<ActionResult<Pet>> Delete (int id){
            Pet pet = await db.Pet
                      .Where(k => k.id_pet == id)
                      .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });
            }
            db.Pet.Remove(pet);

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
                pet
            });
        }
    }
}