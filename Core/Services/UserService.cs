namespace Storeify.Core.Services
{
    public class UserService : IService<ApplicationUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.UserRepository.FindAsync(id);
        }
        public async Task<ApplicationUser?> GetSingleAsync()
        {
            return await _unitOfWork.UserRepository.GetSingleAsync();
        }

        public async Task<ApplicationUser?> GetSingleAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<ApplicationUser>?> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ApplicationUser>?> GetAllActiveAsync()
        {
            var user = await _unitOfWork.UserRepository.GetAllAsync(s => s.IsDeleted == false);
            return user;
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ApplicationUser>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.UserRepository.GetAllIncludingAsync(includes);
        }

        public async Task<ApplicationUser>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.UserRepository.GetWithIncludingAsync(id, includes)!;
        }


    }
}


