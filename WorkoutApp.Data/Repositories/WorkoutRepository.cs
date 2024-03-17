using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories.Base;

namespace WorkoutApp.Data.Repositories
{
    public class WorkoutRepository : BaseRepository<Workout>
    {
        SQLiteAsyncConnection Database;

        public WorkoutRepository() : base()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Workout>();
        }

        public async Task<Workout> GetByIdAsync(int id)
        {
            await Init();

            return await Database.Table<Workout>().Where(w => w.Id == id).FirstOrDefaultAsync();
        }
    }
}
