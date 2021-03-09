using Map.Core.Entities;
using Map.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Map.Infrastructure.Data
{
    public class MapDbContext : DbContext
    {
        public MapDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(ModelBuilderConfigurationsHelper.OnModelCreating(modelBuilder));
        }

        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Point> Points { get; set; }
    }
}
