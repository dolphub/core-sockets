using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Users.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

        // Users
        public DbSet<User> Users { get; set; }
    }
}