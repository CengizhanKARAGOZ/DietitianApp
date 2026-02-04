using System.Linq.Expressions;
using DietApp.Application.Common.Interfaces;
using DietApp.Domain.Common;
using DietApp.Domain.Entities;
using DietApp.Domain.Entities.Clients;
using DietApp.Domain.Entities.Identity;
using DietApp.Domain.Entities.Import;
using DietApp.Domain.Entities.Nutrition;
using DietApp.Domain.Entities.Reports;
using DietApp.Domain.Entities.Subscription;
using DietApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    // Identity
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<TenantBrandSettings> TenantBrandSettings => Set<TenantBrandSettings>();

    // Subscription
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<SubscriptionRecord> Subscriptions => Set<SubscriptionRecord>();
    public DbSet<PaymentNotification> PaymentNotifications => Set<PaymentNotification>();

    // Clients
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<CheckIn> CheckIns => Set<CheckIn>();
    public DbSet<ClientFile> ClientFiles => Set<ClientFile>();
    public DbSet<InteractionNote> InteractionNotes => Set<InteractionNote>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    // Nutrition
    public DbSet<MealPlan> MealPlans => Set<MealPlan>();
    public DbSet<MealPlanDay> MealPlanDays => Set<MealPlanDay>();
    public DbSet<MealPlanItem> MealPlanItems => Set<MealPlanItem>();

    // Reports
    public DbSet<ReportTemplate> ReportTemplates => Set<ReportTemplate>();
    public DbSet<ReportExport> ReportExports => Set<ReportExport>();

    // Import
    public DbSet<ImportJob> ImportJobs => Set<ImportJob>();
    public DbSet<ImportRowError> ImportRowErrors => Set<ImportRowError>();

    // Audit
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global query filter: Soft Delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(CreateSoftDeleteFilter(entityType.ClrType));
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.Id == Guid.Empty)
                    {
                        entry.Entity.Id = Guid.NewGuid();
                    }
                    entry.Entity.CreatedAt = _dateTimeService.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTimeService.UtcNow;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _currentUserService.UserId;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = _dateTimeService.UtcNow;
                entry.Entity.DeletedBy = _currentUserService.UserId;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private static LambdaExpression CreateSoftDeleteFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
        var condition = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(condition, parameter);
    }
}
