using SQLite;

namespace WorkoutApp.Data.Models
{
    public class ExerciseBatch
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ExerciseId { get; set; }

        [SQLite.Ignore]
        public Exercise Exercise { get; set; }

        public int? WorkoutId { get; set; }

        [SQLite.Ignore]
        public Workout Workout { get; set; }
        [SQLite.Ignore]
        public List<ExerciseSet> Sets { get; set; }

        public ExerciseBatch()
        {
            Sets = new List<ExerciseSet>();
        }
    }
}
