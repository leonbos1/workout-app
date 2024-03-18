using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WorkoutApp.Data.Repositories;
using WorkoutApp.Pages;
using WorkoutApp.ViewModels;

namespace WorkoutApp
{
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

            builder.Services.AddSingleton<HistoryPageViewModel>();
            builder.Services.AddSingleton<ProfilePageViewModel>();

            builder.Services.AddSingleton<WorkoutRepository>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<HistoryPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
