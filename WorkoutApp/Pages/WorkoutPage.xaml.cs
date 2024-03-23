using System.Timers;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class WorkoutPage : ContentPage
{
    public const uint AnimationTime = 500;
    private System.Timers.Timer _workoutTimer;
    private TimeSpan _workoutDuration;
    private WorkoutPageViewModel _viewModel;

    public WorkoutPage(WorkoutRepository workoutRepository)
    {
        InitializeComponent();

        AddExerciseContent.ExerciseAdded += OnExerciseAdded;

        _viewModel = new WorkoutPageViewModel();

        BindingContext = _viewModel;
    }

    private void OnExerciseAdded(object sender, Exercise e)
    {
        Console.WriteLine($"Exercise Added: {e.Name}");

        _viewModel.Sets.Add(new ExerciseSet
        {
            Exercise = e,
            Reps = 0,
            Weight = 0,
            SetNumber = _viewModel.Sets.Count + 1,
            IsNewSet = true,
            IsExistingSet = false
        });

        Console.WriteLine($"Sets Count: {_viewModel.Sets.Count}");

        AddExerciseContent.IsVisible = false;
    }

    private async void OnNewWorkoutClicked(object sender, EventArgs e)
    {
        _workoutTimer.Start();
        _workoutDuration = TimeSpan.Zero;

        NewWorkoutContent.IsVisible = true;

        await NewWorkoutContent.TranslateTo(0, 0, AnimationTime, Easing.SinIn);
    }

    private async void OnCloseNewWorkoutClicked(object sender, EventArgs e)
    {
        OngoingWorkoutContent.IsVisible = true;

        await NewWorkoutContent.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height - 100, AnimationTime, Easing.SinOut);
    }

    private async void OnEndWorkoutClicked(object sender, EventArgs e)
    {
        _workoutTimer.Stop();

        OngoingWorkoutContent.IsVisible = false;

        await NewWorkoutContent.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, AnimationTime, Easing.SinOut);
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var set = (ExerciseSet)button.BindingContext;

        _viewModel.AddNewSet(set.Weight, set.Reps);

        Console.WriteLine($"Reps added: {set.Reps}");
        Console.WriteLine($"Weight added: {set.Weight}");
    }

    private async void OnAddTemplateClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Add Template Clicked");
    }

    private async void OnResumeWorkoutClicked(object sender, EventArgs e)
    {
        NewWorkoutContent.IsVisible = true;
        OngoingWorkoutContent.IsVisible = false;

        await NewWorkoutContent.TranslateTo(0, 0, AnimationTime, Easing.SinIn);
    }

    private async void OnAddExerciseClicked(object sender, EventArgs e)
    {
        AddExerciseContent.IsVisible = true;

        await AddExerciseContent.TranslateTo(0, 0, AnimationTime, Easing.SinIn);
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        _workoutDuration = _workoutDuration.Add(TimeSpan.FromSeconds(1));

        MainThread.BeginInvokeOnMainThread(() =>
        {
            TimerLabel.Text = _workoutDuration.ToString(@"hh\:mm\:ss");
            TimerLabel2.Text = _workoutDuration.ToString(@"hh\:mm\:ss");
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        NewWorkoutContent.TranslationY = screenHeight;

        if (_workoutDuration == TimeSpan.Zero)
        {
            _workoutDuration = TimeSpan.Zero;
            _workoutTimer = new System.Timers.Timer(1000);
            _workoutTimer.Elapsed += OnTimedEvent;
            _workoutTimer.AutoReset = true;
            _workoutTimer.Enabled = true;
        }
        else
        {
            OngoingWorkoutContent.IsVisible = true;
        }
    }
}