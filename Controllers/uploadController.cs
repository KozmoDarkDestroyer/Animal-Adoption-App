using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uploadController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public uploadController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPut("[action]/{type}/{id}"), DisableRequestSizeLimit]
        public IActionResult Upload(string type, int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", type);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    if (type == "User")
                    {
                        User user = db.User.Where(k => k.id_user == id).FirstOrDefault();

                        deleteFile(user.img);

                        user.img = dbPath;

                        try
                        {
                            db.SaveChanges();
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
                    }

                    else if (type == "Pet")
                    {
                        Pet pet = db.Pet.Where(k => k.id_pet == id).FirstOrDefault();

                        if (pet.img.Length > 0)
                        {
                            deleteFile(pet.img);
                        }

                        pet.img = dbPath;

                        try
                        {
                            db.SaveChanges();
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
                    }

                    else if (type == "Form")
                    {
                        Form form = db.Form.Where(k => k.id_form == id).FirstOrDefault();

                        if (form.report.Length > 0)
                        {
                            deleteFile(form.report);
                        }

                        form.report = dbPath;

                        try
                        {
                            db.SaveChanges();
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

                    }

                    return Ok(new { dbPath,  });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private void deleteFile(string path){
            bool existPath = System.IO.File.Exists(Path.GetFullPath(path));
            if (existPath == true)
            {
                System.IO.File.Delete(Path.GetFullPath(path));
            }
        }
    }
}