using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class ProfilePage : ContentPage
{
    private WorkoutRepository _workoutRepository;
    private ProfilePageViewModel _viewModel;

    public ProfilePage(WorkoutRepository workoutRepository, ProfilePageViewModel viewModel)
    {
        InitializeComponent();

        _workoutRepository = workoutRepository;
        _viewModel = viewModel;

        BindingContext = _viewModel;

        _workoutRepository.GetNumOfWorkouts().ConfigureAwait(false);
    }

    private async void OnAddWorkout(object sender, EventArgs e)
    {
        var workouts = await _workoutRepository.GetAllAsync();

        Console.WriteLine("Workout added");
    }

    private async void OnRemoveWorkouts(object sender, EventArgs e)
    {
        await _workoutRepository.DeleteAllAsync();
        Console.WriteLine("Workouts removed");
    }

    private async void OnRemoveSets(object sender, EventArgs e)
    {
        await _workoutRepository.DeleteAllSetsAsync();
        Console.WriteLine("Sets removed");
    }

    private async void OnRemoveAll(object sender, EventArgs e)
    {
        await _workoutRepository.DeleteAllSetsAsync();
        await _workoutRepository.DeleteAllAsync();
        await _workoutRepository.DeleteAllBatchesAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.NumOfWorkouts = await _workoutRepository.GetNumOfWorkouts();
    }
}

