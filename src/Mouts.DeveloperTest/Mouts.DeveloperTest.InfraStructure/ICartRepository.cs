using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public interface ICartRepository
    {
        Task<Cart?> GetById(CartId id);

        Task Add(Cart cart);

        Task Update(Cart cart);

        Task Delete(Cart cart);

        Task<PagedResult<Cart>> GetAll(int page, int size, string? order);
    }
}
