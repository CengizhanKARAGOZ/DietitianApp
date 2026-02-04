using DietApp.Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ReportExportConfiguration : IEntityTypeConfiguration<ReportExport>
{
    public void Configure(EntityTypeBuilder<ReportExport> builder)
    {
        builder.ToTable("ReportExports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.TemplateName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.FileKey)
            .HasMaxLength(500);

        builder.Property(r => r.FileName)
            .HasMaxLength(256);

        builder.Property(r => r.Parameters)
            .HasMaxLength(2000);

        builder.HasIndex(r => r.TenantId);
        builder.HasIndex(r => r.ClientId);
        builder.HasIndex(r => r.TemplateId);

        builder.HasOne(r => r.Template)
            .WithMany(t => t.Exports)
            .HasForeignKey(r => r.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Client)
            .WithMany()
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
