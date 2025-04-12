namespace Storeify.Data.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Store> StoreRepository { get; }
        IRepository<Branch> BranchRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Inventory> InventoryRepository { get; }
        IRepository<Product> ProductRepository { get; }
        Task<int> CompleteAsync();
    }
}
