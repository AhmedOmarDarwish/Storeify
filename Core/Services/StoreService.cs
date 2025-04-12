
namespace Storeify.Core.Services
{
    public class StoreService : IService<Store>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _unitOfWork.StoreRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.StoreRepository.FindAsync(id);
        }
        public async Task<Store?> GetSingleAsync()
        {
            return await _unitOfWork.StoreRepository.GetSingleAsync();
        }

        public async Task<Store?> GetSingleAsync(Expression<Func<Store, bool>> predicate)
        {
            return await _unitOfWork.StoreRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Store>?> GetAllAsync()
        {
            return await _unitOfWork.StoreRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Store>?> GetAllActiveAsync()
        {
            var Storees = await _unitOfWork.StoreRepository.GetAllAsync(s => s.IsDeleted == false);
            return Storees;
        }

        public async Task CreateAsync(Store Store)
        {
            await _unitOfWork.StoreRepository.AddAsync(Store);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Store Store)
        {
            _unitOfWork.StoreRepository.Update(Store);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.StoreRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Store>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.StoreRepository.GetAllIncludingAsync(includes);
        }

        public async Task<Store>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.StoreRepository.GetWithIncludingAsync(id, includes)!;
        }


    }
}
