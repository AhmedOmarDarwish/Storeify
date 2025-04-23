namespace Storeify.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Store> StoreRepository { get; }
        public IRepository<Branch> BranchRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Inventory> InventoryRepository { get; }
        public IRepository<Product> ProductRepository { get; }
        public IRepository<ApplicationUser> UserRepository { get; }
        public IRepository<IdentityRole> RoleRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            StoreRepository = new Repository<Store>(_context);
            BranchRepository = new Repository<Branch>(_context);
            CategoryRepository = new Repository<Category>(_context);
            InventoryRepository = new Repository<Inventory>(_context);
            ProductRepository = new Repository<Product>(_context);
            UserRepository = new Repository<ApplicationUser>(_context);
            RoleRepository = new Repository<IdentityRole>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            var rows =  await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return rows;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
