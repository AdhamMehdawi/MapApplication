using Map.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Map.Infrastructure.Data.Configurations.ShapeConfiguration
{
    public class ShapeConfiguration: IEntityTypeConfiguration<Shape>
    {
    public void Configure(EntityTypeBuilder<Shape> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasQueryFilter(c => c.IsDeleted == false);
        
        builder.Property(c => c.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.FillOpacity)
            .HasDefaultValue(1.0);

        builder.Property(c => c.FillColorCode)
            .HasDefaultValue(1.0);
        builder.Property(c => c.Geodesic).HasDefaultValue(false);
    }
    }
}
