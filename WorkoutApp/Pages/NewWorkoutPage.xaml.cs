using System.Timers;

namespace WorkoutApp.Pages;

public partial class NewWorkoutPage : ContentPage
{
    private System.Timers.Timer _timer;
    private DateTime _startTime;

    public NewWorkoutPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartTimer();
    }

    private void StartTimer()
    {
        _startTime = DateTime.Now;

        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        var timeElapsed = DateTime.Now - _startTime;

        Dispatcher.Dispatch(() =>
        {
            timerLabel.Text = timeElapsed.ToString(@"hh\:mm\:ss");
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _timer?.Stop();
        _timer?.Dispose();
    }
}