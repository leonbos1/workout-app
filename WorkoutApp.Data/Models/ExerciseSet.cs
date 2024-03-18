namespace WorkoutApp.Data.Models
{
    public class ExerciseSet
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        public int Reps { get; set; }

        public int Weight { get; set; }

        public int ExerciseId { get; set; }

        [SQLite.Ignore]
        public Exercise Exercise { get; set; }
    }
}