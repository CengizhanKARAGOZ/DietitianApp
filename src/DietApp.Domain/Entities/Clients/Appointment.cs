using DietApp.Domain.Common;
using DietApp.Domain.Enums;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class Appointment : TenantEntity, IAuditable
{
    public Guid ClientId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentType Type { get; set; } = AppointmentType.FollowUp;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? Title { get; set; }
    public string? Notes { get; set; }
    public string? Location { get; set; }
    public bool ReminderSent { get; set; }
    public DateTime? ReminderSentAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public string? CancellationReason { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual Client Client { get; set; } = null!;

    // Computed
    public int DurationMinutes => (int)(EndTime - StartTime).TotalMinutes;
}
