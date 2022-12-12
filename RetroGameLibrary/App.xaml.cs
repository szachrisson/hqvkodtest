using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RetroGamesLibraryHelpers.Helpers;
using RetroGamesLibraryHelpers.Managers;

namespace RetroGameLibrary
{

    public partial class App
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    // Transient used in preparation for multi session/thread support, could be singleton for now
                    services.AddTransient<IFileHelper, FileHelper>();
                    services.AddTransient<ILibraryManager, LibraryManager>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.TimerService.Cancel();
            base.OnExit(e);
        }

    }
}
