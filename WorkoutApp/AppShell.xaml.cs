using WorkoutApp.Pages;

namespace WorkoutApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("newworkout", typeof(NewWorkoutPage));
        }
    }
}
