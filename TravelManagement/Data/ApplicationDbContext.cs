using Microsoft.EntityFrameworkCore;
using TravelManagement.Models;

namespace TravelManagement.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Airline> Airlines { get; set; }
    }
}
