using Microsoft.EntityFrameworkCore;

namespace LoadsheddingV1.Models
{
    public class LoadSheddingContext : DbContext
    {
        public LoadSheddingContext(DbContextOptions<LoadSheddingContext> options)
            : base(options)
        {
        }

        public DbSet<LoadsheddingEvent>? LoadSheddingEvents { get; set; }
        public DbSet<Labs>? Labs { get; set; }
    }
}
