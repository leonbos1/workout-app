using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class HistoryPage : ContentPage
{
    public HistoryPage(WorkoutRepository workoutRepository)
    {
        InitializeComponent();

        BindingContext = new HistoryPageViewModel(workoutRepository);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var viewModel = BindingContext as HistoryPageViewModel;

        if (viewModel != null)
        {
            viewModel.LoadWorkouts();
        }
    }
}