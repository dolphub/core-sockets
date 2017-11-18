using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libs.Ipc;

namespace Base.Controllers
{
    // Route Token, named value from controller
    [Route("/")]
    public class Base : Controller
    {
        private const string VERSION = "1.0.0";
        private readonly string BASE = $"Chat Service {VERSION}";
        public Base(IEventBus eventBus) {
            _eventBus = eventBus;
        }

        private readonly IEventBus _eventBus;

        // GET api/values
        [HttpGet]
        public string Get()
        {
            _eventBus.publish("Chat - EventBusMessage", "chat.test");
            return BASE;
        }
    }
}
