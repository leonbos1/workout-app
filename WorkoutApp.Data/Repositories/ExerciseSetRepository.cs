using SQLite;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories.Base;

namespace WorkoutApp.Data.Repositories
{
    public class ExerciseSetRepository : BaseRepository<ExerciseSet>
    {
        SQLiteAsyncConnection Database;

        public ExerciseSetRepository() : base()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<ExerciseSet>();
        }

        public async Task<ExerciseSet> GetByIdAsync(int id)
        {
            await Init();

            return await Database.Table<ExerciseSet>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ExerciseSet>> GetAllAsync()
        {
            await Init();

            return await Database.Table<ExerciseSet>().ToListAsync();
        }

        public async Task<int> InsertAsync(ExerciseSet exercise)
        {
            await Init();

            return await Database.InsertAsync(exercise);
        }

        public async Task DeleteAllAsync()
        {
            await Init();

            await Database.DeleteAllAsync<ExerciseSet>();
        }
    }
}
