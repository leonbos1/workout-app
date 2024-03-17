using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutApp.Data.Models;

namespace WorkoutApp.Data.Repositories.Base
{
    public class BaseRepository<T> : IRepository where T : class, new()
    {
        SQLiteAsyncConnection Database;

        public BaseRepository()
        {
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            var result = await Database.CreateTableAsync<Workout>();
        }


        public async Task<int> DeleteAsync<T>(T item) where T : class
        {
            await Init();

            return await Database.DeleteAsync(item);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class, new()
        {
            await Init();

            return await Database.Table<T>().ToListAsync();
        }

        public async Task<int> SaveAsync<T>(T item) where T : class
        {
            await Init();

            return await Database.InsertAsync(item);
        }

        public async Task<int> UpdateAsync<T>(T item) where T : class
        {
            await Init();

            return await Database.UpdateAsync(item);
        }
    }
}
