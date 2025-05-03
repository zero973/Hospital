using Hospital.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data;

internal class AppDbContext : DbContext
{
    
    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Disease> Diseases { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // dotnet ef migrations add Initialize -p Hospital.Infrastructure -s Hospital.UI
        // dotnet ef database update -p Hospital.Infrastructure -s Hospital.UI
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
    
}