namespace Storeify.Data.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Store> StoreRepository { get; }
        IRepository<Branch> BranchRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Inventory> InventoryRepository { get; }
        Task<int> CompleteAsync();
    }
}
