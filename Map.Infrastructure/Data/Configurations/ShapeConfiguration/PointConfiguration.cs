using Map.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Map.Infrastructure.Data.Configurations.ShapeConfiguration
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(c => c.IsDeleted == false);
            builder.HasOne(c => c.Shape)
                .WithMany(c => c.ShapePoints)
                .HasForeignKey(key => key.ShapeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(c => c.Lat).IsRequired();
            builder.Property(c => c.Lng).IsRequired();
         }
    }
}
