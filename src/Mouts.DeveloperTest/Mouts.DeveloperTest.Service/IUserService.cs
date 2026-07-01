using Mouts.DeveloperTest.Shared.Dtos.User;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Service
{
    public interface IUserService
    {
        Task<PagedResult<UserResponse>> GetAll(
        int page,
        int size,
        string? order);

        Task<UserResponse?> GetById(int id);

        Task<CreateUserResponse> Create(CreateUserRequest request);

        Task<UserResponse> Update(
    int id,
    UpdateUserRequest request);

        Task<UserResponse> Delete(int id);
    }
}
