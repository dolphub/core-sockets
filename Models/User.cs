using System;

namespace core_sockets.Models
{
    public class User
    {
        public User() {
            CreatedAt = DateTime.Now;
        }

        public long id { get; set; }

        public string username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}