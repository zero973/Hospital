using Hospital.Core.Interfaces;
using Hospital.Core.Modules;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Data.Config;
using Hospital.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure;

public class ModuleInstaller : IModuleInstaller
{

    public byte Order => 1;

    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseNpgsql(configuration.GetConnectionString("Postgres"), options =>
            {
                options.MigrationsAssembly(typeof(AppDbContext).Assembly);
                options.MigrationsHistoryTable(Constants.DbConstants.MigrationsHistoryTableName, 
                    Constants.DbConstants.PublicSchema);
            });
            opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            opts.ReplaceService<IHistoryRepository, CustomHistoryRepository>();
            opts.UseSnakeCaseNamingConvention();

#if DEBUG
            opts.EnableSensitiveDataLogging();
#endif

            opts.UseSeeding((context, _) =>
            {
                new DataSeed().Seed((AppDbContext)context);
                context.SaveChanges();
            });

            opts.UseAsyncSeeding(async (context, _, token) =>
            {
                new DataSeed().Seed((AppDbContext)context);
                await context.SaveChangesAsync(token);
            });
        });

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ModuleInstaller).Assembly);
        });
    }

}