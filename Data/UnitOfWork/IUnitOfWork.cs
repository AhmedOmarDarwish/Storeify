namespace Storeify.Data.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Store> StoreRepository { get; }
        IRepository<Branch> BranchRepository { get; }
        Task<int> CompleteAsync();
    }
}
