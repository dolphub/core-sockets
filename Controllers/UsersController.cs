using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core_sockets.Models;

namespace core_sockets.Controllers
{
    // Route Token, named value from controller
    [Route("[controller]")]
    public class UsersController : Controller
    {
        public UsersController(ApiContext context)
        {
            _context = context;
        }

        private readonly ApiContext _context;

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {            
            return _context.Users.ToList();
        }

        // GET users/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(string id)
        {
            var user = _context.Users.FirstOrDefault(v => v.id == id);
            if (user == null) {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody][Required]User user)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtRoute("GetUser", new { id = user.id }, user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
