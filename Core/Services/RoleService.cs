using Microsoft.AspNetCore.Identity;

namespace Storeify.Core.Services
{
    public class RoleService : IService<IdentityRole>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityRole?> GetByIdAsync(int id)
        {
            return await _unitOfWork.RoleRepository.GetByIdAsync(id);
        }

        public async Task<bool?> FindAsync(int id)
        {
            return await _unitOfWork.RoleRepository.FindAsync(id);
        }

        public async Task<IdentityRole?> GetSingleAsync()
        {
            return await _unitOfWork.RoleRepository.GetSingleAsync();
        }
        public async Task<IdentityRole?> GetSingleAsync(Expression<Func<IdentityRole, bool>> predicate)
        {
            return await _unitOfWork.RoleRepository.GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<IdentityRole>?> GetAllAsync()
        {
            return await _unitOfWork.RoleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<IdentityRole>?> GetAllActiveAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return roles;
        }

        public async Task CreateAsync(IdentityRole role)
        {
            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(IdentityRole role)
        {
             _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
              _unitOfWork.RoleRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<IdentityRole>?> GetAllIncludingAsync(params string[] includes)
        {
            return await _unitOfWork.RoleRepository.GetAllIncludingAsync(includes);
        }

        public async Task<IdentityRole>? GetWithIncludingAsync(int id, params string[] includes)
        {
            return await _unitOfWork.RoleRepository.GetWithIncludingAsync(id, includes)!;
        }

    }
}
