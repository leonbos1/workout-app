using System.Collections.ObjectModel;
using System.ComponentModel;
using WorkoutApp.Data.Models;

namespace WorkoutApp.ViewModels
{
    public class WorkoutPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExerciseSet> Sets { get; set; } = new ObservableCollection<ExerciseSet>();

        public WorkoutPageViewModel()
        {
            Sets.Add(new ExerciseSet { SetNumber = 1, Weight = 100, Reps = 10, Exercise = new Exercise { Name = "Bench Press" }, IsNewSet = false });
            Sets.Add(new ExerciseSet { SetNumber = 2, IsNewSet = true });
        }

        public void AddNewSet(int weight, int reps)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
