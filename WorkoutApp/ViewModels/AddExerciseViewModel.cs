using System.Collections.ObjectModel;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories;

namespace WorkoutApp.ViewModels
{
    public class AddExerciseViewModel
    {
        private readonly ExerciseRepository _exerciseRepository;
        public ObservableCollection<Exercise> Exercises { get; set; }

        public AddExerciseViewModel(ExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;

            Exercises = new ObservableCollection<Exercise>();

            MainThread.BeginInvokeOnMainThread(LoadExercises);
        }

        public async void LoadExercises()
        {
            var exercises = await _exerciseRepository.GetAllAsync();

            Exercises.Clear();

            foreach (var exercise in exercises)
            {
                Exercises.Add(exercise);
                Console.WriteLine("Name: " + exercise.Name);
            }
        }
    }
}
