using CDNPortalTutorial.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDNPortalTutorial.Data
{
    public class ApplicationDbContext : DbContext
    {
        // CTOR
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Add-Migration "Initiall Migration"
        public DbSet<User> Users { get; set; }
    }
}
