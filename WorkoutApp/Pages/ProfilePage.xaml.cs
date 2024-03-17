namespace WorkoutApp.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        BindingContext = new ProfileViewModel();
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Settings");
    }
}

public class ProfileViewModel : BindableObject
{
    private string _pageTitle = "Profile";

    public string PageTitle
    {
        get { return _pageTitle; }
        set
        {
            if (_pageTitle != value)
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }
    }
}