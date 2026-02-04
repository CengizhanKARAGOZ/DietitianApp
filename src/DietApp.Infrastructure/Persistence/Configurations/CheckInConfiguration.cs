using DietApp.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
{
    public void Configure(EntityTypeBuilder<CheckIn> builder)
    {
        builder.ToTable("CheckIns");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.WaterIntakeLiters).HasPrecision(4, 2);
        builder.Property(c => c.SleepHours).HasPrecision(4, 2);

        builder.Property(c => c.Challenges).HasMaxLength(1000);
        builder.Property(c => c.Achievements).HasMaxLength(1000);
        builder.Property(c => c.DietitianNotes).HasMaxLength(2000);

        builder.HasIndex(c => c.TenantId);
        builder.HasIndex(c => c.ClientId);
        builder.HasIndex(c => c.CheckInDate);

        builder.HasOne(c => c.Client)
            .WithMany(cl => cl.CheckIns)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
