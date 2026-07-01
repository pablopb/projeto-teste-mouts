using Mouts.DeveloperTest.Shared.Dtos.Cart;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Service
{
    public interface ICartService
    {
        Task<CartResponse> Create(CreateCartRequest request);

        Task<CartResponse?> GetById(int id);

        Task<PagedResult<CartResponse>> GetAll(int page, int size, string? order);

        Task<CartResponse> Update(int id, CreateCartRequest request);

        Task<CartResponse> Delete(int id);
    }
}
