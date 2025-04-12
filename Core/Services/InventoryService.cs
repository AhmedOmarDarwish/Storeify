
namespace Storeify.Core.Services
{
    public class InventoryService : IService<Inventory>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _unitOfWork.InventoryRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.InventoryRepository.FindAsync(id);
        }

        public async Task<Inventory?> GetSingleAsync()
        {
            return await _unitOfWork.InventoryRepository.GetSingleAsync();
        }
        public async Task<Inventory?> GetSingleAsync(Expression<Func<Inventory, bool>> predicate)
        {
            return await _unitOfWork.InventoryRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Inventory>?> GetAllAsync()
        {
            return await _unitOfWork.InventoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Inventory>?> GetAllActiveAsync()
        {
            var Inventoryes = await _unitOfWork.InventoryRepository.GetAllAsync(s => s.IsDeleted == false);
            return Inventoryes;
        }

        public async Task CreateAsync(Inventory Inventory)
        {
            await _unitOfWork.InventoryRepository.AddAsync(Inventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Inventory Inventory)
        {
             _unitOfWork.InventoryRepository.Update(Inventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
              _unitOfWork.InventoryRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Inventory>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.InventoryRepository.GetAllIncludingAsync(includes);
        }

        public async Task<Inventory>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.InventoryRepository.GetWithIncludingAsync(id, includes)!;
        }

    }
}
