
namespace Storeify.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetSingleAsync();
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<bool> FindAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void DeleteAsync(int id);
        Task<IEnumerable<T>?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<int> RowCountAsync();

        //Use Include for Eager Loading
        Task<IEnumerable<T>?> GetAllIncludingAsync(params string[] includes);
        Task<T>? GetWithIncludingAsync(int id,params string[] includes);
    }
}
