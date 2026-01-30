using DietApp.Domain.Common;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class InteractionNote : TenantEntity, IAuditable
{
    public Guid ClientId { get; set; }
    public DateTime InteractionDate { get; set; }
    public string InteractionType { get; set; } = "FaceToFace";
    public string? Subject { get; set; }
    public string Content { get; set; } = string.Empty;
    public int? DurationMinutes { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client Client { get; set; } = null!;
}
