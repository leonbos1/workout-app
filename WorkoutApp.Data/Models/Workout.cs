using SQLite;

namespace WorkoutApp.Data.Models
{
    public class Workout
    {
        [PrimaryKey]
        public int? Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartedAt { get; set; }

        public string PrettyStartedAt => StartedAt.ToString("MMMM dd");

        public string PrettyEndedAt => EndedAt.ToString("MMMM dd");

        public TimeSpan Duration => EndedAt - StartedAt;

        public string PrettyDuration => $"{Duration.Hours}h {Duration.Minutes}m";

        public double _totalWeight
        {
            get
            {
                var totalWeight = 0.0;

                foreach (var batch in Batches)
                {
                    foreach (var set in batch.Sets)
                    {
                        totalWeight += set.Weight * set.Reps ?? 0;
                    }
                }
                return totalWeight;
            }
        }

        public double TotalWeight { get; set;}

        public int AmountOfPrs => 1;

        public DateTime EndedAt { get; set; }

        [SQLite.Ignore]
        public List<ExerciseBatch> Batches { get; set; }

        public Workout()
        {
            Batches = new List<ExerciseBatch>();
        }
    }
}
