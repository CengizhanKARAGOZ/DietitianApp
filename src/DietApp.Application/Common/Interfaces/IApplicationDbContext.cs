using DietApp.Domain.Entities;
using DietApp.Domain.Entities.Clients;
using DietApp.Domain.Entities.Identity;
using DietApp.Domain.Entities.Import;
using DietApp.Domain.Entities.Nutrition;
using DietApp.Domain.Entities.Reports;
using DietApp.Domain.Entities.Subscription;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // Identity
    DbSet<Tenant> Tenants { get; }
    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<TenantBrandSettings> TenantBrandSettings { get; }

    // Subscription
    DbSet<Plan> Plans { get; }
    DbSet<SubscriptionRecord> Subscriptions { get; }
    DbSet<PaymentNotification> PaymentNotifications { get; }

    // Clients
    DbSet<Client> Clients { get; }
    DbSet<Measurement> Measurements { get; }
    DbSet<CheckIn> CheckIns { get; }
    DbSet<ClientFile> ClientFiles { get; }
    DbSet<InteractionNote> InteractionNotes { get; }
    DbSet<Appointment> Appointments { get; }

    // Nutrition
    DbSet<MealPlan> MealPlans { get; }
    DbSet<MealPlanDay> MealPlanDays { get; }
    DbSet<MealPlanItem> MealPlanItems { get; }

    // Reports
    DbSet<ReportTemplate> ReportTemplates { get; }
    DbSet<ReportExport> ReportExports { get; }

    // Import
    DbSet<ImportJob> ImportJobs { get; }
    DbSet<ImportRowError> ImportRowErrors { get; }

    // Audit
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
