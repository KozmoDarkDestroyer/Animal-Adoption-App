using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace animal_adoption.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class imageController : ControllerBase
    {
        [HttpGet("{root}/{type}/{file}")]
        public IActionResult BannerImage(string root, string type, string file)
        {  
            bool existPath = System.IO.File.Exists(Path.GetFullPath(root + "/" + type + "/" + file));

            if (existPath == true)
            {
                string filePath = Path.GetFullPath(root + "/" + type + "/" + file);
                // System.IO.File.Delete("Uploads/User/Natsumi_dal.jpg");
                return PhysicalFile(filePath,"image/png");
            }
            else{
                string noImage = Path.GetFullPath("Uploads/No-image/no-image.jpg");
                return PhysicalFile(noImage,"image/png");
            }
        }
    }
}