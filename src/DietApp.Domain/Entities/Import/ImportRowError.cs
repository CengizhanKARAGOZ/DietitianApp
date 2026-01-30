using DietApp.Domain.Common;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Import;

public class ImportRowError : BaseEntity
{
    public Guid ImportJobId { get; set; }
    public int RowNumber { get; set; }
    public ImportRowStatus Status { get; set; }
    public string? RawData { get; set; }
    public string? ErrorMessage { get; set; }
    public string? FieldName { get; set; }
    public Guid? MatchedClientId { get; set; }
    public bool IsDuplicate { get; set; }
    public DuplicateResolution? DuplicateResolution { get; set; }

    // Navigation
    public virtual ImportJob ImportJob { get; set; } = null!;
}
