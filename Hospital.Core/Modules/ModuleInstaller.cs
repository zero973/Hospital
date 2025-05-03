using FluentValidation;
using Hospital.Core.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Core.Modules;

public class ModuleInstaller : IModuleInstaller
{

    public byte Order => 2;

    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<ModuleInstaller>(includeInternalTypes: true);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ModuleInstaller).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

}