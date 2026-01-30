using DietApp.Domain.Common;
using DietApp.Domain.Enums;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Reports;

public class ReportTemplate : TenantEntity, ISoftDeletable, IAuditable
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public LanguageCode LanguageCode { get; set; } = LanguageCode.TrTR;
    public string TemplateContent { get; set; } = "[]";
    public int Version { get; set; } = 1;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;
    public string PageSize { get; set; } = "A4";
    public string PageOrientation { get; set; } = "Portrait";
    public int MarginTop { get; set; } = 20;
    public int MarginBottom { get; set; } = 20;
    public int MarginLeft { get; set; } = 20;
    public int MarginRight { get; set; } = 20;

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual ICollection<ReportExport> Exports { get; set; } = new List<ReportExport>();
}
