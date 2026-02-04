using DietApp.Domain.Entities.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
{
    public void Configure(EntityTypeBuilder<MealPlanItem> builder)
    {
        builder.ToTable("MealPlanItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.MealType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(i => i.FoodName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Portion)
            .HasMaxLength(100);

        builder.Property(i => i.Protein).HasPrecision(6, 2);
        builder.Property(i => i.Carbs).HasPrecision(6, 2);
        builder.Property(i => i.Fat).HasPrecision(6, 2);
        builder.Property(i => i.Fiber).HasPrecision(6, 2);

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        builder.Property(i => i.Alternatives)
            .HasMaxLength(1000);

        builder.HasIndex(i => i.MealPlanDayId);
        builder.HasIndex(i => i.TenantId);

        builder.HasOne(i => i.MealPlanDay)
            .WithMany(d => d.Items)
            .HasForeignKey(i => i.MealPlanDayId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
