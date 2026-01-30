using DietApp.Domain.Common;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Identity;

public class Tenant : BaseEntity, ISoftDeletable
{
    public string BusinessName { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string PreferredLanguage { get; set; } = "tr-TR";
    public string TimeZone { get; set; } = "Europe/Istanbul";
    public bool IsActive { get; set; } = true;

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    // Navigation
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual TenantBrandSettings? BrandSettings { get; set; }
}
