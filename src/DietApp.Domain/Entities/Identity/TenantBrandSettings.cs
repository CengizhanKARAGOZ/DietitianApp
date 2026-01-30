using DietApp.Domain.Common;

namespace DietApp.Domain.Entities.Identity;

public class TenantBrandSettings : BaseEntity
{
    public Guid TenantId { get; set; }
    public string? LogoFileKey { get; set; }
    public string? LogoFileName { get; set; }
    public string? LogoContentType { get; set; }
    public long? LogoFileSize { get; set; }
    public string PrimaryColor { get; set; } = "#2563eb";
    public string SecondaryColor { get; set; } = "#64748b";
    public string? ReportFooterText { get; set; }
    public string? KvkkDisclosureText { get; set; }

    // Navigation
    public virtual Tenant Tenant { get; set; } = null!;
}
