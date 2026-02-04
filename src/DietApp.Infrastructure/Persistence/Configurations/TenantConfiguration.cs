using DietApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.TaxNumber)
            .HasMaxLength(20);

        builder.Property(t => t.Phone)
            .HasMaxLength(20);

        builder.Property(t => t.Address)
            .HasMaxLength(500);

        builder.Property(t => t.City)
            .HasMaxLength(100);

        builder.Property(t => t.PreferredLanguage)
            .IsRequired()
            .HasMaxLength(10)
            .HasDefaultValue("tr-TR");

        builder.Property(t => t.TimeZone)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Europe/Istanbul");

        builder.HasIndex(t => t.IsActive);
        builder.HasIndex(t => t.IsDeleted);
    }
}
