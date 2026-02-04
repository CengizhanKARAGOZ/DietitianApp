using DietApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Action)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.UserEmail)
            .HasMaxLength(256);

        builder.Property(a => a.EntityType)
            .HasMaxLength(100);

        builder.Property(a => a.OldValues)
            .HasColumnType("json");

        builder.Property(a => a.NewValues)
            .HasColumnType("json");

        builder.Property(a => a.IpAddress)
            .HasMaxLength(45);

        builder.Property(a => a.UserAgent)
            .HasMaxLength(512);

        builder.Property(a => a.Details)
            .HasMaxLength(2000);

        builder.Property(a => a.ErrorMessage)
            .HasMaxLength(2000);

        builder.HasIndex(a => a.TenantId);
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.Action);
        builder.HasIndex(a => a.CreatedAt);
        builder.HasIndex(a => a.EntityType);
    }
}
