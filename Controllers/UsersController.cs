using System;
using System.Collections.Generic;
using System.Linq;
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
        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(v => v.id == id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (user == null) {
                return BadRequest();
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
