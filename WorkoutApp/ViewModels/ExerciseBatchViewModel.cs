using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkoutApp.Data.Models;

namespace WorkoutApp.ViewModels
{
    public class ExerciseBatchViewModel : INotifyPropertyChanged
    {
        public Exercise Exercise { get; set; }
        public ObservableCollection<ExerciseSetViewModel> Sets { get; } = new ObservableCollection<ExerciseSetViewModel>();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExerciseBatchViewModel(Exercise exercise)
        {
            Exercise = exercise;
        }

        public ExerciseBatch ToModel()
        {
            return new ExerciseBatch
            {
                Exercise = Exercise,
                Sets = Sets.Select(set => set.ToModel()).ToList()
            };
        }
    }
}
