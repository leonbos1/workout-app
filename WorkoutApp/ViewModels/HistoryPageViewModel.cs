using System.Collections.ObjectModel;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;

namespace WorkoutApp.ViewModels
{
    public class HistoryPageViewModel
    {
        private readonly WorkoutRepository _workoutRepository;
        public ObservableCollection<Workout> Workouts { get; set; }

        public HistoryPageViewModel(WorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;

            Workouts = new ObservableCollection<Workout>();

            MainThread.BeginInvokeOnMainThread(LoadWorkouts);
        }

        public async void LoadWorkouts()
        {
            var workouts = await _workoutRepository.GetAllAsync();

            Workouts.Clear();

            foreach (var workout in workouts)
            {
                Workouts.Add(workout);
            }
            
            Console.WriteLine("Amount of workouts: " + Workouts.Count);
        }
    }
}
