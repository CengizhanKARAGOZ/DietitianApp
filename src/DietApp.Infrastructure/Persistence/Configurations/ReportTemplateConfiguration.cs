using DietApp.Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ReportTemplateConfiguration : IEntityTypeConfiguration<ReportTemplate>
{
    public void Configure(EntityTypeBuilder<ReportTemplate> builder)
    {
        builder.ToTable("ReportTemplates");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.Property(r => r.LanguageCode)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.TemplateContent)
            .IsRequired()
            .HasColumnType("json");

        builder.Property(r => r.PageSize)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("A4");

        builder.Property(r => r.PageOrientation)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("Portrait");

        builder.HasIndex(r => r.TenantId);
        builder.HasIndex(r => r.IsDeleted);
    }
}
