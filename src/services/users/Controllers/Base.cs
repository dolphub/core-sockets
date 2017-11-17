using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Users.Controllers
{
    // Route Token, named value from controller
    [Route("/")]
    public class Base : Controller
    {
        private const string VERSION = "1.0.0";
        private readonly string BASE = $"Users Service {VERSION}";
        public Base() {}        

        // GET api/values
        [HttpGet]
        public string Get()
        {            
            return BASE;
        }
    }
}
