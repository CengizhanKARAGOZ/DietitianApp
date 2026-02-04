using DietApp.Domain.Entities.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Period)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.FeatureFlags)
            .HasMaxLength(2000);

        builder.HasIndex(p => p.Code).IsUnique();
        builder.HasIndex(p => p.IsActive);
    }
}
