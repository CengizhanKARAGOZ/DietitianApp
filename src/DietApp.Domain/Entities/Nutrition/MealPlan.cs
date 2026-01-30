using DietApp.Domain.Common;
using DietApp.Domain.Entities.Clients;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Nutrition;

public class MealPlan : TenantEntity, ISoftDeletable, IAuditable
{
    public Guid? ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? TargetCalories { get; set; }
    public int? TargetProtein { get; set; }
    public int? TargetCarbs { get; set; }
    public int? TargetFat { get; set; }
    public bool IsTemplate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client? Client { get; set; }
    public virtual ICollection<MealPlanDay> Days { get; set; } = new List<MealPlanDay>();
}
