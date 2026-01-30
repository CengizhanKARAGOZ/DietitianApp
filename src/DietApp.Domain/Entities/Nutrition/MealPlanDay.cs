using DietApp.Domain.Common;

namespace DietApp.Domain.Entities.Nutrition;

public class MealPlanDay : TenantEntity
{
    public Guid MealPlanId { get; set; }
    public int DayNumber { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public virtual MealPlan MealPlan { get; set; } = null!;
    public virtual ICollection<MealPlanItem> Items { get; set; } = new List<MealPlanItem>();
}
