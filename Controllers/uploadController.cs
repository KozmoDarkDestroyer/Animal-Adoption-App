using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using animal_adoption.context;
using animal_adoption.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uploadController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private string dbPath;
        public uploadController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPut("[action]/{type}/{id}"), DisableRequestSizeLimit, Authorize(Roles = "ADMIN,USER")]
        public IActionResult Upload(string type, int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", type);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                Guid myGuid = Guid.NewGuid();

                if (file.Length > 0)
                {
                    var fileName = id + myGuid.ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    dbPath = Path.Combine(folderName, fileName);
                    var extension = Path.GetExtension(dbPath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    if (type == "User" && extension == ".jpg" || extension == ".jpeg"
                        || extension == ".png" || extension == ".gif")
                    {
                        User user = db.User.Where(k => k.id_user == id).FirstOrDefault();

                        if (user == null)
                        {
                            deleteFile(dbPath);
                            return NotFound(new {
                                ok = false,
                                err = "The id " + id + " does not exist in the records"
                            });   
                        }

                        if (user.img == null)
                        {
                            user.img = dbPath;
                        }

                        else{
                            deleteFile(user.img);
                            user.img = dbPath;
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (System.Exception err)
                        {
                            deleteFile(dbPath);
                            return BadRequest(new {
                                ok = true,
                                err = new {
                                    message = err.InnerException.Message
                                }
                            });
                        }          
                    }

                    else if (type == "Pet" && extension == ".jpg" || extension == ".jpeg"
                        || extension == ".png" || extension == ".gif")
                    {
                        Pet pet = db.Pet.Where(k => k.id_pet == id).FirstOrDefault();

                        if (pet == null)
                        {
                            deleteFile(dbPath);
                            return NotFound(new {
                            ok = false,
                            err = "The id " + id + " does not exist in the records"
                            });   
                        }

                        if (pet.img == null)
                        {
                            pet.img = dbPath;
                        }

                        else{
                            deleteFile(pet.img);
                            pet.img = dbPath;
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (System.Exception err)
                        {
                            deleteFile(dbPath);
                            return BadRequest(new {
                                ok = false,
                                err = new {
                                    message = err.InnerException.Message
                                }
                            });
                        }     
                    }

                    else if (type == "Form" && extension == ".pdf")
                    {
                        Form form = db.Form.Where(k => k.id_form == id).FirstOrDefault();

                        if (form == null)
                        {
                            deleteFile(dbPath);
                            return NotFound(new {
                            ok = false,
                            err = "The id " + id + " does not exist in the records"
                        });   
            }
                        if (form.report == null)
                        {
                            form.report = dbPath;
                        }

                        else{
                            deleteFile(form.report);
                            form.report = dbPath;
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (System.Exception err)
                        {
                            deleteFile(dbPath);
                            return BadRequest(new {
                                ok = false,
                                err = new {
                                    message = err.InnerException.Message
                                }
                            });
                        }    

                    }

                    else{
                        deleteFile(dbPath);
                        return BadRequest(new {
                            ok = false,
                            err = "The valid extensions for the images are .jpeg, .png, .jpg, .gif and for the files it is .pdf."
                        });
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    deleteFile(dbPath);
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                deleteFile(dbPath);
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