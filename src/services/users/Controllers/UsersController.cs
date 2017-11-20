using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Users.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Libs.Ipc;

namespace Users.Controllers
{
    // Route Token, named value from controller
    [Route("[controller]")]
    public class UsersController : Controller
    {
        public UsersController(ApiContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        private readonly ApiContext _context;
        private readonly IEventBus _eventBus;

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        // GET users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(v => v.id == id);
            if (user == null) {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required]User user)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            _eventBus.publish(user.id, "user.created");
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
