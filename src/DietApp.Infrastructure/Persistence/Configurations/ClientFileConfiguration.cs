using DietApp.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ClientFileConfiguration : IEntityTypeConfiguration<ClientFile>
{
    public void Configure(EntityTypeBuilder<ClientFile> builder)
    {
        builder.ToTable("ClientFiles");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(f => f.OriginalFileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(f => f.FileKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Category)
            .HasMaxLength(50);

        builder.Property(f => f.Description)
            .HasMaxLength(500);

        builder.HasIndex(f => f.TenantId);
        builder.HasIndex(f => f.ClientId);

        builder.HasOne(f => f.Client)
            .WithMany(c => c.Files)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
