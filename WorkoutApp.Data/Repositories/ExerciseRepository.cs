using SQLite;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories.Base;

namespace WorkoutApp.Data.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>
    {
        SQLiteAsyncConnection Database;

        public ExerciseRepository() : base()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Exercise>();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            await Init();

            return await Database.Table<Exercise>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Exercise>> GetAllAsync()
        {
            await Init();

            return await Database.Table<Exercise>().ToListAsync();
        }

        public async Task InsertAsync(Exercise exercise)
        {
            await Init();

            await Database.InsertAsync(exercise);
        }

        public async Task DeleteAllAsync()
        {
            await Init();

            await Database.DeleteAllAsync<Exercise>();
        }
    }
}
