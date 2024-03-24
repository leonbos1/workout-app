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
    private WorkoutRepository _workoutRepository;
    private ExerciseBatchRepository _exerciseBatchRepository;
    private ExerciseSetRepository _exerciseSetRepository;
    private Workout Workout { get; set; }

    public WorkoutPage(WorkoutRepository workoutRepository, ExerciseBatchRepository exerciseBatchRepository, ExerciseSetRepository
        exerciseSetRepository)
    {
        InitializeComponent();

        AddExerciseContent.ExerciseAdded += OnExerciseAdded;

        _workoutRepository = workoutRepository;

        Workout = new Workout();

        _viewModel = new WorkoutPageViewModel();

        BindingContext = _viewModel;

        _workoutRepository = workoutRepository;
        _exerciseBatchRepository = exerciseBatchRepository;
        _exerciseSetRepository = exerciseSetRepository;
    }

    private async void OnExerciseAdded(object sender, Exercise e)
    {
        Console.WriteLine($"Exercise Added: {e.Name}");

        var batch = new ExerciseBatchViewModel(e);

        _viewModel.ExerciseBatches.Add(batch);

        AddExerciseContent.IsVisible = false;
    }

    private async void OnNewWorkoutClicked(object sender, EventArgs e)
    {
        _workoutTimer.Start();
        _workoutDuration = TimeSpan.Zero;

        NewWorkoutContent.IsVisible = true;

        Workout.StartedAt = DateTime.Now;

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

        Workout.EndedAt = DateTime.Now;
        Workout.Batches = _viewModel.ExerciseBatches.Select(b => b.ToModel()).ToList();

        foreach (var batch in Workout.Batches)
        {
            await _exerciseBatchRepository.InsertAsync(batch);

            foreach (var set in batch.Sets)
            {
                await _exerciseSetRepository.InsertAsync(set);
            }
        }

        await _workoutRepository.InsertAsync(Workout);

        await NewWorkoutContent.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, AnimationTime, Easing.SinOut);
    }

    private async void OnAddSetButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var parentStackLayout = (StackLayout)button.Parent;
        var repsEntry = (Entry)parentStackLayout.Children.First(c => c is Entry && c.AutomationId == "RepsEntry");
        var weightEntry = (Entry)parentStackLayout.Children.First(c => c is Entry && c.AutomationId == "WeightEntry");

        int reps = int.TryParse(repsEntry.Text, out int r) ? r : 0;
        double weight = double.TryParse(weightEntry.Text, out double w) ? w : 0.0;

        Console.WriteLine($"Add Set Button Clicked: {reps} x {weight}");

        var context = (ExerciseBatchViewModel)button.BindingContext;
        context.Sets.Add(new ExerciseSetViewModel { Reps = reps, Weight = weight });

        repsEntry.Text = string.Empty;
        weightEntry.Text = string.Empty;

        await _exerciseSetRepository.InsertAsync(new ExerciseSet { Reps = reps, Weight = weight, ExerciseBatchId = context. });
    }

    private async void OnDeleteSetButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var context = (ExerciseSetViewModel)button.BindingContext;

        var parentStackLayout = (StackLayout)button.Parent;

        var exerciseBatch = (ExerciseBatchViewModel)parentStackLayout.BindingContext;

        exerciseBatch.Sets.Remove(context);

        Console.WriteLine("Delete Set Button Clicked");
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