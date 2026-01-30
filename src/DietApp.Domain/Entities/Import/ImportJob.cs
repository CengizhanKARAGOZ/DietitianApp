using DietApp.Domain.Common;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Import;

public class ImportJob : TenantEntity
{
    public ImportJobStatus Status { get; set; } = ImportJobStatus.Uploaded;
    public ImportFileType FileType { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileKey { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int TotalRows { get; set; }
    public int ProcessedRows { get; set; }
    public int SuccessCount { get; set; }
    public int SkippedCount { get; set; }
    public int ErrorCount { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ColumnMapping { get; set; }
    public DuplicateResolution DefaultDuplicateResolution { get; set; } = DuplicateResolution.Skip;
    public Guid UploadedByUserId { get; set; }

    // Navigation
    public virtual ICollection<ImportRowError> RowErrors { get; set; } = new List<ImportRowError>();
}
