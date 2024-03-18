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
    }

    private async void OnAddWorkout(object sender, EventArgs e)
    {
        var workout = new Workout
        {
            Name = "Sample Workout",
            Description = "This is a sample workout",
            StartedAt = DateTime.Now,
            EndedAt = DateTime.Now.AddHours(1),
            Sets = new List<ExerciseSet>
            {
                new ExerciseSet
                {
                    Exercise = new Exercise
                    {
                        Name = "Pushups",
                        Description = "Do as many pushups as you can"
                    },
                    Reps = 10,
                    Weight = 0
                },
                new ExerciseSet
                {
                    Exercise = new Exercise
                    {
                        Name = "Squats",
                        Description = "Do as many squats as you can"
                    },
                    Reps = 10,
                    Weight = 0
                }
            }
        };

        await _workoutRepository.InsertAsync(workout);
        Console.WriteLine("Workout added");
    }

    private async void OnRemoveWorkouts(object sender, EventArgs e)
    {
        await _workoutRepository.DeleteAllAsync();
        Console.WriteLine("Workouts removed");
    }
}

