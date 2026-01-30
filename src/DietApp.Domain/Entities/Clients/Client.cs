using System.Diagnostics.Metrics;
using DietApp.Domain.Common;
using DietApp.Domain.Enums;
using DietApp.Domain.Interfaces;

namespace DietApp.Domain.Entities.Clients;

public class Client : TenantEntity, ISoftDeletable, IAuditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Gender Gender { get; set; } = Gender.NotSpecified;
    public int? BirthYear { get; set; }
    public int? BirthMonth { get; set; }
    public decimal? Height { get; set; }
    public decimal? TargetWeight { get; set; }
    public string? GoalDescription { get; set; }
    public string? Allergies { get; set; }
    public string? HealthNotes { get; set; }
    public string? Tags { get; set; }
    public ClientStatus Status { get; set; } = ClientStatus.Active;
    public string? Notes { get; set; }
    public DateTime? FirstConsultationDate { get; set; }
    public DateTime? LastConsultationDate { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    // IAuditable
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation
    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
    public virtual ICollection<ClientFile> Files { get; set; } = new List<ClientFile>();
    public virtual ICollection<InteractionNote> InteractionNotes { get; set; } = new List<InteractionNote>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    // Computed
    public string FullName => $"{FirstName} {LastName}";

    public int? Age
    {
        get
        {
            if (!BirthYear.HasValue)
            {
                return null;
            }

            var today = DateTime.Today;
            int age = today.Year - BirthYear.Value;

            if (BirthMonth.HasValue && BirthMonth.Value > today.Month)
            {
                age--;
            }

            return age;
        }
    }
}
