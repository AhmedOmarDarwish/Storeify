
namespace Storeify.Core.IServices
{
    public interface IService<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<bool?> FindAsync(int id);
        Task<T?> GetSingleAsync();
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<IEnumerable<T>?> GetAllActiveAsync();
        Task CreateAsync(T branch);
        Task UpdateAsync(T branch);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>?> GetAllIncludingAsync(params string[] includes);
        Task<T>? GetWithIncludingAsync(int id, params string[] includes);
    }
}
