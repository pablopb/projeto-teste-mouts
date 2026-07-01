using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public interface IUserRepository
    {
        public Task<User?> GetByUserName(string username);

        Task Add(User user);

        Task<bool> ExistsByUserName(string userName);

        Task<bool> ExistsByEmail(string email);

        Task<User?> GetById(UserId id);

        Task<PagedResult<User>> GetAll(
    int page,
    int size,
    string? order);

        Task Update(User user);

        Task Delete(User user);
    }
}
