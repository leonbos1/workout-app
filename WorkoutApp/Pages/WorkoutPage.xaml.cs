namespace WorkoutApp.Pages;

public partial class WorkoutPage : ContentPage
{
	public WorkoutPage()
	{
		InitializeComponent();
	}

	private async void OnNewWorkout(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("newworkout");
    }

    //OnAddTemplate
	public async void OnAddTemplate(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("addtemplate");
    }

    //OnEditTemplate

}