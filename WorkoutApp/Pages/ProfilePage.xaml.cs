using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class ProfilePage : ContentPage
{
    private WorkoutRepository _workoutRepository;
    private ExerciseRepository _exerciseRepository;
    private ProfilePageViewModel _viewModel;

    public ProfilePage(WorkoutRepository workoutRepository, ProfilePageViewModel viewModel, ExerciseRepository exerciseRepository)
    {
        InitializeComponent();

        _workoutRepository = workoutRepository;
        _exerciseRepository = exerciseRepository;
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

        InitExercises();
    }

    private async void InitExercises()
    {
        var exercises = new List<string>
        {
            "Bench Press (Barbell)",
            "Squat (Barbell)",
            "Deadlift (Barbell)",
            "Overhead Press (Barbell)",
            "Barbell Row (Barbell)"
        };

        foreach (var exercise in exercises)
        {
            var existingExercise = await _exerciseRepository.GetByNameAsync(exercise);

            if (existingExercise == null)
            {
                await _exerciseRepository.InsertAsync(new Exercise { Name = exercise });
            }
        }
    }
}

