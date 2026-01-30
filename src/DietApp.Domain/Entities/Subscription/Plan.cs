using DietApp.Domain.Common;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Subscription;

public class Plan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public PlanPeriod Period { get; set; }
    public decimal Price { get; set; }
    public int ClientLimit { get; set; }
    public int StaffLimit { get; set; }
    public int StorageLimitMb { get; set; }
    public string? FeatureFlags { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
    public bool IsFeatured { get; set; }

    // Navigation
    public virtual ICollection<SubscriptionRecord> Subscriptions { get; set; } = new List<SubscriptionRecord>();
}
