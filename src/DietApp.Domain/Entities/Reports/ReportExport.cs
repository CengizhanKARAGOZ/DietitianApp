using DietApp.Domain.Common;
using DietApp.Domain.Entities.Clients;

namespace DietApp.Domain.Entities.Reports;

public class ReportExport : TenantEntity
{
    public Guid ClientId { get; set; }
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public DateTime? DateRangeStart { get; set; }
    public DateTime? DateRangeEnd { get; set; }
    public string? FileKey { get; set; }
    public string? FileName { get; set; }
    public long? FileSize { get; set; }
    public string? Parameters { get; set; }
    public Guid GeneratedByUserId { get; set; }

    // Navigation
    public virtual ReportTemplate Template { get; set; } = null!;
    public virtual Client Client { get; set; } = null!;
}
