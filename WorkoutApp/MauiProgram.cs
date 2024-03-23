using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WorkoutApp.Data.Repositories;
using WorkoutApp.Pages;
using WorkoutApp.ViewModels;

namespace WorkoutApp
{
    public static class MauiProgram
    {
        static IServiceProvider serviceProvider;

        public static TService GetService<TService>()
            => serviceProvider.GetService<TService>();

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
            builder.Services.AddSingleton<ExerciseRepository>();

            builder.Services.AddTransient<AddExerciseView>();

            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<WorkoutPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            serviceProvider = app.Services;

            return app;
        }
    }
}
