using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace core_sockets.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

        // Users
        public DbSet<User> Users { get; set; }
    }
}