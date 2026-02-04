using DietApp.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.Title)
            .HasMaxLength(200);

        builder.Property(a => a.Notes)
            .HasMaxLength(2000);

        builder.Property(a => a.Location)
            .HasMaxLength(300);

        builder.Property(a => a.CancellationReason)
            .HasMaxLength(500);

        builder.HasIndex(a => a.TenantId);
        builder.HasIndex(a => a.ClientId);
        builder.HasIndex(a => a.StartTime);
        builder.HasIndex(a => a.Status);

        builder.HasOne(a => a.Client)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(a => a.DurationMinutes);
    }
}
