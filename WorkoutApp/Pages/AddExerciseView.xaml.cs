using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;
using WorkoutApp.ViewModels;

namespace WorkoutApp.Pages;

public partial class AddExerciseView : ContentView
{
	public event EventHandler<Exercise> ExerciseAdded;

	public AddExerciseView()
	{
		InitializeComponent();

		BindingContext = new AddExerciseViewModel(MauiProgram.GetService<ExerciseRepository>());
	}

	private async void OnCloseClicked(object sender, EventArgs e)
	{
        await this.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, WorkoutPage.AnimationTime, Easing.SinOut);
    }

	private void OnAddClicked(object sender, EventArgs e)
	{
        Exercise selectedExercise = (Exercise)(sender as Button).CommandParameter;

		ExerciseAdded?.Invoke(this, selectedExercise);
    }
}