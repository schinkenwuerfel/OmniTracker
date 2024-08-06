using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OmniTracker.DataContext;

namespace OmniTracker;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddDbContext<TrackerContext>(options =>
        {
            var directory = FileSystem.AppDataDirectory;
            var dbPath = Path.Join(directory, "sqlite.db");
            options.UseSqlite($"Data Source={dbPath}");
        });
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        var context = app.Services.GetRequiredService<TrackerContext>();
        context.Database.Migrate();

        return app;
    }
}
