 
using Map.Infrastructure.Data.Configurations.ShapeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Map.Infrastructure.Data.Configurations
{
    public class ModelBuilderConfigurationsHelper
    {
        public static ModelBuilder OnModelCreating(ModelBuilder modelBuilder)
        {
            //========== Map Entity Model configuration================ 
            modelBuilder.ApplyConfiguration(new ShapeConfiguration.ShapeConfiguration());
            modelBuilder.ApplyConfiguration(new PointConfiguration());
            return modelBuilder;
        }
    }
}
