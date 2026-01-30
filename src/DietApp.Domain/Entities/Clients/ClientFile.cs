using DietApp.Domain.Common;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class ClientFile : TenantEntity, IAuditable
{
    public Guid ClientId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileKey { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client Client { get; set; } = null!;
}
