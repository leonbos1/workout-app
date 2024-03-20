using System.Timers;

namespace WorkoutApp.Pages;

public partial class WorkoutPage : ContentPage
{
    private const uint animationTime = 500;
    private System.Timers.Timer _workoutTimer;
    private TimeSpan _workoutDuration;

    public WorkoutPage()
    {
        InitializeComponent();
    }

    private async void OnNewWorkoutClicked(object sender, EventArgs e)
    {
        _workoutTimer.Start();
        _workoutDuration = TimeSpan.Zero;

        NewWorkoutContent.IsVisible = true;

        await NewWorkoutContent.TranslateTo(0, 0, animationTime, Easing.SinIn);
    }

    private async void OnCloseNewWorkoutClicked(object sender, EventArgs e)
    {
        OngoingWorkoutContent.IsVisible = true;

        await NewWorkoutContent.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height - 100, animationTime, Easing.SinOut);
    }

    private async void OnEndWorkoutClicked(object sender, EventArgs e)
    {
        _workoutTimer.Stop();

        OngoingWorkoutContent.IsVisible = false;

        await NewWorkoutContent.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, animationTime, Easing.SinOut);
    }

    private async void OnAddTemplateClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Add Template Clicked");
    }

    private async void OnResumeWorkoutClicked(object sender, EventArgs e)
    {
        NewWorkoutContent.IsVisible = true;
        OngoingWorkoutContent.IsVisible = false;

        await NewWorkoutContent.TranslateTo(0, 0, animationTime, Easing.SinIn);
    }

    private async void OnAddExerciseClicked(object sender, EventArgs e)
    {
        var addExercisePage = new AddExercisePage();

        await Navigation.PushAsync(addExercisePage);
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        _workoutDuration = _workoutDuration.Add(TimeSpan.FromSeconds(1));

        MainThread.BeginInvokeOnMainThread(() =>
        {
            TimerLabel.Text = _workoutDuration.ToString(@"hh\:mm\:ss");
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
    }
}