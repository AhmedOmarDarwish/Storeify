namespace Storeify.Core.Services
{
    public class ProductService : IService<Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.ProductRepository.FindAsync(id);
        }

        public async Task<Product?> GetSingleAsync()
        {
            return await _unitOfWork.ProductRepository.GetSingleAsync();
        }
        public async Task<Product?> GetSingleAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _unitOfWork.ProductRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>?> GetAllActiveAsync()
        {
            var Products = await _unitOfWork.ProductRepository.GetAllAsync(s => s.IsDeleted == false);
            return Products;
        }

        public async Task CreateAsync(Product Product)
        {
            await _unitOfWork.ProductRepository.AddAsync(Product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Product Product)
        {
            _unitOfWork.ProductRepository.Update(Product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.ProductRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Product>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.ProductRepository.GetAllIncludingAsync(includes);
        }

        public async Task<Product>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.ProductRepository.GetWithIncludingAsync(id, includes)!;
        }


    }
}
