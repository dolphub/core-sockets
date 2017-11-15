using System;
using System.ComponentModel.DataAnnotations;

namespace core_sockets.Models
{
    public class User
    {
        public User() {
            CreatedAt = DateTime.Now;
            id = Guid.NewGuid().ToString();
        }

        public string id { get; set; }

        [Required(ErrorMessage = "Username required")]
        public string username { get; set; }
        public DateTime CreatedAt { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email address required")]
        public string emailAddress { get; set; }
    }
}