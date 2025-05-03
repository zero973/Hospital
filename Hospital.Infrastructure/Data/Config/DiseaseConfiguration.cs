using Hospital.Core.Models.Entities;
using Hospital.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Data.Config;

internal class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
{
    public void Configure(EntityTypeBuilder<Disease> builder)
    {
        builder.Metadata.SetSchema(DbConstants.KernelSchema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PeriodType)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.HasIndex(e => e.PeriodType);

        builder.HasIndex(e => new { e.Name, e.PeriodType })
            .IsUnique();
    }
}