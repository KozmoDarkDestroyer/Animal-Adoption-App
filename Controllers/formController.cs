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
    public class formController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public formController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost("[action]")]

        public async Task<ActionResult<FormPost>> Create ([FromBody] FormPost model){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Form form = new Form();
            await db.Form.AddAsync(AssignsControllers.AssigForm(model,form));
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
                form
            });
        }

        [HttpPut("[action]/{id}")]

        public async Task<ActionResult<FormPost>> Update ([FromBody] FormPost model, int id){
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            Form form = await db.Form
                        .Where(k => k.id_form == id)
                        .FirstOrDefaultAsync();
            AssignsControllers.AssigForm(model,form);
            if (form == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
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
                form
            });
        }

        [HttpGet("[action]/{id}")]

        public async Task<ActionResult<FormPost>> Get (int id){
            Form form = await db.Form
                        .Include(k => k.Adopter)
                        .Where(k => k.id_form == id)
                        .FirstOrDefaultAsync();
            if (form == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
            return Ok(new {
                ok = true,
                form
            });
        }

        [HttpGet("[action]")]

        public async Task<ActionResult<FormPost>> List(){
            List<Form> forms = await db.Form
                               .Include(k => k.Adopter)
                               .ToListAsync();
            if (forms == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "There are no records in the database"
                });  
            }
            return Ok(new {
                ok = true,
                forms
            });
        }

        [HttpDelete("[action]/{id}")]

        public async Task<ActionResult<FormPost>> Delete(int id){
            Form form = await db.Form
                        .Where(k => k.id_form == id)
                        .FirstOrDefaultAsync();
            if (form == null)
            {
                return NotFound(new {
                    ok = false,
                    err = "The id " + id + " does not exist in the records"
                });   
            }
            db.Form.Remove(form);
            return Ok(new {
                ok = true,
                form
            });
        }
    }
}