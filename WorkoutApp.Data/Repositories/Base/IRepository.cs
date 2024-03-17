namespace WorkoutApp.Data.Repositories.Base
{
    public interface IRepository
    {
        Task<List<T>> GetAllAsync<T>() where T : class, new();
        Task<int> SaveAsync<T>(T item) where T : class;
        Task<int> DeleteAsync<T>(T item) where T : class;
        Task<int> UpdateAsync<T>(T item) where T : class;
    }
}
