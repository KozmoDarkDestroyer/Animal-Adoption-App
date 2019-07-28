using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Functions;
using animal_adoption.Models;
using animal_adoption.ModelViews;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class foundationController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public foundationController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<FoundationPost>> Create([FromBody] FoundationPost model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
             
            Foundation foundation = new Foundation();
            await db.AddAsync(AssignsControllers.AssingFoundation(model,foundation));

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
                foundation
            });
        }

        [HttpPut("[action]/{id}")]

        public async Task<ActionResult<FoundationPost>> Update ([FromBody] FoundationPost model, int id){

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            Foundation foundation = await db.Foundation
                                    .Where(k => k.id_foundation == id)
                                    .FirstOrDefaultAsync(); 

            if (foundation == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
            
            AssignsControllers.AssingFoundation(model,foundation);

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
                foundation
            });
        }

        [HttpGet("[action]/{id}"),/* Authorize(Roles = "Manager") */]

        public async Task<ActionResult<Foundation>> Get (int id){

            Foundation foundation = await db.Foundation
                                    .Where(k => k.id_foundation == id)
                                    .FirstOrDefaultAsync();
            if (foundation == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });
            }
            return Ok(new{
                ok = true,
                foundation
            });
        }

        [HttpGet("[action]")]

        public async Task<ActionResult<Foundation>> List(){

            List<Foundation> foundations = await db.Foundation.ToListAsync();
            if (foundations.Count == 0)
            {
                return NotFound(new {
                    ok = false,
                    err = "There are no records in the database"
                });
            }
            return Ok(new {
                ok = true,
                foundations
            });
        }

        [HttpDelete("[action]/{id}")]

        public async Task<ActionResult<Foundation>> Delete (int id){

            Foundation foundation = await db.Foundation
                                    .Where(k => k.id_foundation == id)
                                    .FirstOrDefaultAsync();
            if (foundation == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });
            }

            db.Remove(foundation);

            try
            {
                await db.SaveChangesAsync();   
            }
            catch (System.Exception)
            {
                return BadRequest(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });
            }
            return Ok(new {
                ok = true,
                foundation
            });
        }
    }
}