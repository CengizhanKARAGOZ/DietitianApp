using DietApp.Domain.Entities.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ImportJobConfiguration : IEntityTypeConfiguration<ImportJob>
{
    public void Configure(EntityTypeBuilder<ImportJob> builder)
    {
        builder.ToTable("ImportJobs");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(i => i.FileType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(i => i.OriginalFileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(i => i.FileKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(i => i.ErrorMessage)
            .HasMaxLength(2000);

        builder.Property(i => i.ColumnMapping)
            .HasColumnType("json");

        builder.Property(i => i.DefaultDuplicateResolution)
            .HasConversion<int>();

        builder.HasIndex(i => i.TenantId);
        builder.HasIndex(i => i.Status);
    }
}
