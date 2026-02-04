using DietApp.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietApp.Infrastructure.Persistence.Configurations;

public class InteractionNoteConfiguration : IEntityTypeConfiguration<InteractionNote>
{
    public void Configure(EntityTypeBuilder<InteractionNote> builder)
    {
        builder.ToTable("InteractionNotes");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.InteractionType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(n => n.Subject)
            .HasMaxLength(200);

        builder.Property(n => n.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasIndex(n => n.TenantId);
        builder.HasIndex(n => n.ClientId);
        builder.HasIndex(n => n.InteractionDate);

        builder.HasOne(n => n.Client)
            .WithMany(c => c.InteractionNotes)
            .HasForeignKey(n => n.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
