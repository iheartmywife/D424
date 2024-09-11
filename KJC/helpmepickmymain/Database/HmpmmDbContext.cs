using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Database
{
    public class HmpmmDbContext : DbContext
    {
        public HmpmmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Faction> Factions { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Spec> Specs { get; set; }
        public DbSet<WowClass> WowClasses { get; set; }

    }

}
