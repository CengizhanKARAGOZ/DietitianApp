using DietApp.Domain.Entities.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ImportRowErrorConfiguration : IEntityTypeConfiguration<ImportRowError>
{
    public void Configure(EntityTypeBuilder<ImportRowError> builder)
    {
        builder.ToTable("ImportRowErrors");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.RawData)
            .HasMaxLength(4000);

        builder.Property(r => r.ErrorMessage)
            .HasMaxLength(1000);

        builder.Property(r => r.FieldName)
            .HasMaxLength(100);

        builder.Property(r => r.DuplicateResolution)
            .HasConversion<int?>();

        builder.HasIndex(r => r.ImportJobId);

        builder.HasOne(r => r.ImportJob)
            .WithMany(j => j.RowErrors)
            .HasForeignKey(r => r.ImportJobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
