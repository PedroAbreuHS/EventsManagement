using EventsManagement.Presentation.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Presentation.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected AppDbContext() { }

        public DbSet<Event> Events { get; set; }


    }
}
