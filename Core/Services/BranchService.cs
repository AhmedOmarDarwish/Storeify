
namespace Storeify.Core.Services
{
    public class BranchService : IService<Branch>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Branch?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BranchRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.BranchRepository.FindAsync(id);
        }

        public async Task<Branch?> GetFirstAsync()
        {
            return await _unitOfWork.BranchRepository.GetFirstAsync();
        }
        public async Task<Branch?> GetFirstAsync(Expression<Func<Branch, bool>> predicate)
        {
            return await _unitOfWork.BranchRepository.GetFirstAsync(predicate);
        }

        public async Task<IEnumerable<Branch>?> GetAllAsync()
        {
            return await _unitOfWork.BranchRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Branch>?> GetAllActiveAsync()
        {
            var branches = await _unitOfWork.BranchRepository.GetAllAsync(s => s.IsDeleted == false);
            return branches;
        }

        public async Task CreateAsync(Branch branch)
        {
            await _unitOfWork.BranchRepository.AddAsync(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Branch branch)
        {
             _unitOfWork.BranchRepository.Update(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
              _unitOfWork.BranchRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Branch>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.BranchRepository.GetAllIncludingAsync(includes);
        }

        public async Task<Branch>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.BranchRepository.GetWithIncludingAsync(id, includes)!;
        }

    }
}
