using DietApp.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class TenantBrandSettingsConfiguration : IEntityTypeConfiguration<TenantBrandSettings>
{
    public void Configure(EntityTypeBuilder<TenantBrandSettings> builder)
    {
        builder.ToTable("TenantBrandSettings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.LogoFileKey)
            .HasMaxLength(500);

        builder.Property(b => b.LogoFileName)
            .HasMaxLength(256);

        builder.Property(b => b.LogoContentType)
            .HasMaxLength(100);

        builder.Property(b => b.PrimaryColor)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("#2563eb");

        builder.Property(b => b.SecondaryColor)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("#64748b");

        builder.Property(b => b.ReportFooterText)
            .HasMaxLength(1000);

        builder.Property(b => b.KvkkDisclosureText)
            .HasMaxLength(2000);

        builder.HasIndex(b => b.TenantId).IsUnique();

        builder.HasOne(b => b.Tenant)
            .WithOne(t => t.BrandSettings)
            .HasForeignKey<TenantBrandSettings>(b => b.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
