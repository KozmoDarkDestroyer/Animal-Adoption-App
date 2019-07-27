using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animal_adoption.context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}