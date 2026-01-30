using DietApp.Domain.Common;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class CheckIn : TenantEntity, IAuditable
{
    public Guid ClientId { get; set; }
    public DateTime CheckInDate { get; set; }
    public decimal? WaterIntakeLiters { get; set; }
    public int? StepCount { get; set; }
    public decimal? SleepHours { get; set; }
    public int? SleepQuality { get; set; }
    public int? DietCompliance { get; set; }
    public int? ExerciseCompliance { get; set; }
    public int? EnergyLevel { get; set; }
    public int? StressLevel { get; set; }
    public int? HungerLevel { get; set; }
    public string? Challenges { get; set; }
    public string? Achievements { get; set; }
    public string? DietitianNotes { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client Client { get; set; } = null!;
}
