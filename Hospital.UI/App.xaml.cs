using Hospital.Core.Commands.Database;
using Hospital.Core.Interfaces;
using Hospital.UI.Extensions;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using Hospital.UI.Services.Impl;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Hospital.UI;

public partial class App : Application
{

    private IHost _host = null!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        using var cursor = new WaitCursor();

        var builder = Host.CreateApplicationBuilder();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var installers = new List<Core.Modules.IModuleInstaller>
        {
            new Core.Modules.ModuleInstaller(),
            new Infrastructure.ModuleInstaller()
        };

        foreach (var installer in installers.OrderBy(x => x.Order))
            installer.Install(builder.Services, configuration);

        builder.Services.AddSingleton<Windows.MainWindow>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IUserStore, UserStore>();

        builder.Services.RegisterPagesAndModels();

        _host = builder.Build();
        await _host.StartAsync();

        var sender = _host.Services.GetRequiredService<ISender>();
        var result = await sender.Send(new MigrateDatabase());
        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage("Произошла ошибка при выполнении миграций");
            Current.Shutdown();
        }

        cursor.Dispose();

        var mainWindow = _host.Services.GetRequiredService<Windows.MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            _host.StopAsync(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
        }

        base.OnExit(e);
    }

}