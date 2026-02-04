using DietApp.Domain.Entities.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class MealPlanDayConfiguration : IEntityTypeConfiguration<MealPlanDay>
{
    public void Configure(EntityTypeBuilder<MealPlanDay> builder)
    {
        builder.ToTable("MealPlanDays");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DayOfWeek)
            .HasConversion<int?>();

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.Property(d => d.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(d => d.MealPlanId);
        builder.HasIndex(d => d.TenantId);

        builder.HasOne(d => d.MealPlan)
            .WithMany(m => m.Days)
            .HasForeignKey(d => d.MealPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
