using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class AddExercise : ContentView
{
	public AddExercise()
	{
        InitializeComponent();

        BindingContext = new AddExerciseViewModel(MauiProgram.GetService<ExerciseRepository>());
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
		await this.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, WorkoutPage.animationTime, Easing.SinOut);
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        //print the selected exercise CommandParameter="{Binding .}" />
        Console.WriteLine("Selected exercise: " + (sender as Button).CommandParameter);

        Exercise exercise = (Exercise)(sender as Button).CommandParameter;

        Console.WriteLine("Selected exercise: " + exercise.Name);

    }
}