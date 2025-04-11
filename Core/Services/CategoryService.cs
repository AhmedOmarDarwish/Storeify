namespace Storeify.Core.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.FindAsync(id);
        }

        public async Task<Category?> GetSingleAsync()
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync();
        }
        public async Task<Category?> GetSingleAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Category>?> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>?> GetAllActiveAsync()
        {
            var Categoryes = await _unitOfWork.CategoryRepository.GetAllAsync(s => s.IsDeleted == false);
            return Categoryes;
        }

        public async Task CreateAsync(Category Category)
        {
            await _unitOfWork.CategoryRepository.AddAsync(Category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Category Category)
        {
             _unitOfWork.CategoryRepository.Update(Category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
              _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Category>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetAllIncludingAsync(includes);
        }

        public async Task<Category>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetWithIncludingAsync(id, includes)!;
        }

    }
}
