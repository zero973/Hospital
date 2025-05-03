using Hospital.Core.Models.Entities;
using Hospital.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Data.Config;

internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Metadata.SetSchema(DbConstants.DataSchema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FIO)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Birthday)
            .IsRequired();
        
        builder.Property(x => x.Snils)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(e => e.FIO);

        builder.HasIndex(e => e.Snils)
            .IsUnique();

        builder.HasIndex(e => new { e.FIO, e.Birthday, e.Snils })
            .IsUnique();
    }
}