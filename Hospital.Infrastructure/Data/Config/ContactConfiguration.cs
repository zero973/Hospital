using Hospital.Core.Models.Entities;
using Hospital.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Data.Config;

internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Metadata.SetSchema(DbConstants.DataSchema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(w => w.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Disease>()
            .WithMany()
            .HasForeignKey(w => w.DiseaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(x => x.ResourcesSpent)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(e => new { e.PatientId, e.DiseaseId, e.Date })
            .IsUnique();

        builder.OwnsMany(x => x.ResourcesSpent, rb =>
        {
            rb.WithOwner().HasForeignKey("ContactId");

            rb.Property(r => r.Resource)
                .IsRequired()
                .HasMaxLength(250);

            rb.Property(r => r.Comment)
                .IsRequired()
                .HasColumnType("text")
                .HasMaxLength(250);

            rb.Property(r => r.Count)
                .IsRequired()
                .HasColumnType("integer");

            rb.ToTable("resources_spent", DbConstants.DataSchema);

            rb.HasKey("ContactId", nameof(ResourceSpent.Resource), nameof(ResourceSpent.Comment));

            rb.HasIndex("ContactId");
        });
    }
}