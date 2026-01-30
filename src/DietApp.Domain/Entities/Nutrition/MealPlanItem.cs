using DietApp.Domain.Common;
using DietApp.Domain.Enums;

namespace DietApp.Domain.Entities.Nutrition;

public class MealPlanItem : TenantEntity
{
    public Guid MealPlanDayId { get; set; }
    public MealType MealType { get; set; }
    public int SortOrder { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public string? Portion { get; set; }
    public int? Calories { get; set; }
    public decimal? Protein { get; set; }
    public decimal? Carbs { get; set; }
    public decimal? Fat { get; set; }
    public decimal? Fiber { get; set; }
    public string? Notes { get; set; }
    public string? Alternatives { get; set; }

    // Navigation
    public virtual MealPlanDay MealPlanDay { get; set; } = null!;
}
