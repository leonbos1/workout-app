using SQLite;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories.Base;

namespace WorkoutApp.Data.Repositories
{
    public class ExerciseBatchRepository : BaseRepository<ExerciseBatch>
    {
        SQLiteAsyncConnection Database;

        public ExerciseBatchRepository() : base()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<ExerciseBatch>();
        }

        public async Task<ExerciseBatch> GetByIdAsync(int id)
        {
            await Init();

            return await Database.Table<ExerciseBatch>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ExerciseBatch>> GetAllAsync()
        {
            await Init();

            return await Database.Table<ExerciseBatch>().ToListAsync();
        }

        public async Task<int> InsertAsync(ExerciseBatch exercise)
        {
            await Init();

            return await Database.InsertAsync(exercise);
        }

        public async Task DeleteAllAsync()
        {
            await Init();

            await Database.DeleteAllAsync<ExerciseBatch>();
        }
    }
}
