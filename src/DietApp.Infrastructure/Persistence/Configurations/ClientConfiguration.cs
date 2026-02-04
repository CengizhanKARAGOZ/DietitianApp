using DietApp.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .HasMaxLength(256);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Gender)
            .HasConversion<int>();

        builder.Property(c => c.Height)
            .HasPrecision(5, 2);

        builder.Property(c => c.TargetWeight)
            .HasPrecision(5, 2);

        builder.Property(c => c.GoalDescription)
            .HasMaxLength(500);

        builder.Property(c => c.Allergies)
            .HasMaxLength(1000);

        builder.Property(c => c.HealthNotes)
            .HasMaxLength(2000);

        builder.Property(c => c.Tags)
            .HasMaxLength(500);

        builder.Property(c => c.Notes)
            .HasMaxLength(2000);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(c => c.TenantId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.IsDeleted);
        builder.HasIndex(c => new { c.TenantId, c.Email });

        builder.Ignore(c => c.FullName);
        builder.Ignore(c => c.Age);
    }
}
