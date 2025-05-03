using Microsoft.Extensions.DependencyInjection;

namespace Hospital.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterPagesAndModels(this IServiceCollection services)
    {
        services.AddTransient<Pages.AuthorizationPage>();
        services.AddTransient<PageModels.AuthorizationPageModel>();

        services.AddTransient<Pages.ContactsPage>();
        services.AddTransient<PageModels.ContactsPageModel>();

        services.AddTransient<Pages.DiseasesPage>();
        services.AddTransient<PageModels.DiseasesPageModel>();

        services.AddTransient<Pages.PatientsPage>();
        services.AddTransient<PageModels.PatientsPageModel>();

        services.AddTransient<Pages.ResourcesSpentPage>();
        services.AddTransient<PageModels.ResourcesSpentPageModel>();

        services.AddTransient<Pages.UsersPage>();
        services.AddTransient<PageModels.UsersPageModel>();

        return services;
    }
}