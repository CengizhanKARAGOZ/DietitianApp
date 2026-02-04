using DietApp.Domain.Entities.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class PaymentNotificationConfiguration : IEntityTypeConfiguration<PaymentNotification>
{
    public void Configure(EntityTypeBuilder<PaymentNotification> builder)
    {
        builder.ToTable("PaymentNotifications");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.PaymentDescription)
            .HasMaxLength(500);

        builder.Property(p => p.ReceiptFileKey)
            .HasMaxLength(500);

        builder.Property(p => p.ReceiptFileName)
            .HasMaxLength(256);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.RejectionReason)
            .HasMaxLength(500);

        builder.Property(p => p.AdminNotes)
            .HasMaxLength(1000);

        builder.HasIndex(p => p.SubscriptionId);
        builder.HasIndex(p => p.Status);

        builder.HasOne(p => p.Subscription)
            .WithMany(s => s.PaymentNotifications)
            .HasForeignKey(p => p.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
