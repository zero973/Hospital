using Hospital.Core.Models.Entities;
using Hospital.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Data.Config;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Metadata.SetSchema(DbConstants.DataSchema);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasIndex(e => e.Login)
            .IsUnique();

        builder.HasIndex(e => new { e.Login, e.PasswordHash })
            .IsUnique();
    }
}