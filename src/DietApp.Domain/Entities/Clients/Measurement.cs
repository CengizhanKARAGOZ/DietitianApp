using DietApp.Domain.Common;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class Measurement : TenantEntity, IAuditable
{
    public Guid ClientId { get; set; }
    public DateTime MeasurementDate { get; set; }
    public decimal? Weight { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public decimal? MuscleMass { get; set; }
    public decimal? WaterPercentage { get; set; }
    public decimal? BoneMass { get; set; }
    public int? VisceralFatLevel { get; set; }
    public decimal? Bmi { get; set; }
    public int? Bmr { get; set; }
    public decimal? WaistCircumference { get; set; }
    public decimal? HipCircumference { get; set; }
    public decimal? ChestCircumference { get; set; }
    public decimal? ArmCircumference { get; set; }
    public decimal? ThighCircumference { get; set; }
    public string? Notes { get; set; }
    public Guid? ImportJobId { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client Client { get; set; } = null!;
}
