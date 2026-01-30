using DietApp.Domain.Common;
using DietApp.Domain.Entities.Identity;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Subscription;

public class SubscriptionRecord : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid PlanId { get; set; }
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.PendingPayment;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? TrialEndDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentReferenceCode { get; set; } = string.Empty;
    public bool AutoRenew { get; set; } = true;
    public DateTime? CanceledAt { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? SuspendedAt { get; set; }
    public string? SuspensionReason { get; set; }
    public string? AdminNotes { get; set; }

    // Navigation
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual Plan Plan { get; set; } = null!;
    public virtual ICollection<PaymentNotification> PaymentNotifications { get; set; } = new List<PaymentNotification>();

    // Computed
    public bool IsActive => Status == SubscriptionStatus.Active &&
                            StartDate.HasValue &&
                            EndDate.HasValue &&
                            DateTime.UtcNow >= StartDate.Value &&
                            DateTime.UtcNow <= EndDate.Value;

    public int DaysRemaining => EndDate.HasValue
        ? Math.Max(0, (EndDate.Value - DateTime.UtcNow).Days)
        : 0;
}
