namespace WorkoutApp.Data.Models
{
    public class Workout
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartedAt { get; set; }

        public string PrettyStartedAt => StartedAt.ToString("MMMM dd");

        public string PrettyEndedAt => EndedAt.ToString("MMMM dd");

        public string PrettyDuration
        {
            get
            {
                var duration = EndedAt - StartedAt;
                return $"{duration.Hours}h {duration.Minutes}m";
            }
        }

        public int TotalWeight
        {
            get
            {
                var totalWeight = 0;
                foreach (var set in Sets)
                {
                    totalWeight += set.Weight;
                }
                return totalWeight;
            }
        }

        public int AmountOfPrs => 1;

        public DateTime EndedAt { get; set; }

        [SQLite.Ignore]
        public List<ExerciseSet> Sets { get; set; }

        public Workout()
        {
            Sets = new List<ExerciseSet>();
        }
    }
}
