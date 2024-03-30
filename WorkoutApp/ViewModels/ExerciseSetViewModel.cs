using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkoutApp.Data.Models;

namespace WorkoutApp.ViewModels
{
    public class ExerciseSetViewModel : INotifyPropertyChanged
    {
        private int _setNumber;
        private int _reps;
        private double _weight;

        public int SetNumber
        {
            get => _setNumber;
            set
            {
                _setNumber = value;
                OnPropertyChanged();
            }
        }

        public int Reps
        {
            get => _reps;
            set
            {
                _reps = value;
                OnPropertyChanged();
            }
        }

        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExerciseSet ToModel()
        {
            return new ExerciseSet
            {
                SetNumber = SetNumber,
                Reps = Reps,
                Weight = Weight
            };
        }
    }

}
