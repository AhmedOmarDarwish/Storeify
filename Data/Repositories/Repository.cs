using System;

namespace Storeify.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            try
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<T?> GetSingleAsync()
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync();
        }
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<T?> GetByIdAsync(int id)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));

            var keyName = entityType?.FindPrimaryKey()?.Properties
                .Select(x => x.Name)
                .SingleOrDefault();

            if (string.IsNullOrEmpty(keyName))
                return null;

            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async void DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }
        public async Task<IEnumerable<T>?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }
        public async Task<int> RowCountAsync()
        {
            try
            {
                return await _dbSet.AsNoTracking().CountAsync();
            }
            catch (SqlException)
            {
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<IEnumerable<T>?> GetAllIncludingAsync(params string[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<T>? GetWithIncludingAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _dbSet.Where(e => EF.Property<int>(e, "Id") == id); // Filter by ID

            // Dynamically apply includes
            foreach (var include in includes)
            {
                query = query.Include(include); // Use Include with the property name
            }

            try
            {
                return await query.AsNoTracking().SingleOrDefaultAsync()!;
            }
            catch (Exception)
            {
                return null;
            }
            // Get the single result (or null if not found)
        }
        public async Task<bool> FindAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id); 
                return entity != null; 
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}
