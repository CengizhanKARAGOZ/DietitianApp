using DietApp.Domain.Entities.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
{
    public void Configure(EntityTypeBuilder<MealPlan> builder)
    {
        builder.ToTable("MealPlans");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .HasMaxLength(1000);

        builder.Property(m => m.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(m => m.TenantId);
        builder.HasIndex(m => m.ClientId);
        builder.HasIndex(m => m.IsTemplate);
        builder.HasIndex(m => m.IsDeleted);

        builder.HasOne(m => m.Client)
            .WithMany()
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
