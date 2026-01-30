using DietApp.Domain.Common;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Subscription;

public class PaymentNotification : BaseEntity
{
    public Guid SubscriptionId { get; set; }
    public Guid NotifiedByUserId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentDescription { get; set; }
    public string? ReceiptFileKey { get; set; }
    public string? ReceiptFileName { get; set; }
    public PaymentNotificationStatus Status { get; set; } = PaymentNotificationStatus.Pending;
    public Guid? ProcessedByUserId { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? RejectionReason { get; set; }
    public string? AdminNotes { get; set; }

    // Navigation
    public virtual SubscriptionRecord Subscription { get; set; } = null!;
}
