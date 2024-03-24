using System.ComponentModel;

namespace WorkoutApp.ViewModels
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private int _numOfWorkouts;

        public event PropertyChangedEventHandler PropertyChanged;

        public int NumOfWorkouts
        {
            get => _numOfWorkouts;
            set
            {
                _numOfWorkouts = value;
                OnPropertyChanged(nameof(NumOfWorkouts));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
