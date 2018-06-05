using Microsoft.EntityFrameworkCore;
 
namespace Wedding_Planner.Models
{
    public class WeddingPlannerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingPlannerContext(DbContextOptions<WeddingPlannerContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Wedding> weddings { get; set; }
        public DbSet<Guest> guests { get; set; }
    }
}