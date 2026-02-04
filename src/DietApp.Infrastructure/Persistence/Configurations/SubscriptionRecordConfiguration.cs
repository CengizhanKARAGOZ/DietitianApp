using DietApp.Domain.Entities.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class SubscriptionRecordConfiguration : IEntityTypeConfiguration<SubscriptionRecord>
{
    public void Configure(EntityTypeBuilder<SubscriptionRecord> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(s => s.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(s => s.PaymentReferenceCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.CancellationReason)
            .HasMaxLength(500);

        builder.Property(s => s.SuspensionReason)
            .HasMaxLength(500);

        builder.Property(s => s.AdminNotes)
            .HasMaxLength(1000);

        builder.HasIndex(s => s.TenantId);
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.PaymentReferenceCode).IsUnique();

        builder.HasOne(s => s.Tenant)
            .WithMany()
            .HasForeignKey(s => s.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Plan)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(s => s.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(s => s.IsActive);
        builder.Ignore(s => s.DaysRemaining);
    }
}
