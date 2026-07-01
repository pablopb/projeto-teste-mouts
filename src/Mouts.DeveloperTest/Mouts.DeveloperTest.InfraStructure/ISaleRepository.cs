using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public interface ISaleRepository
    {
        Task<Sale?> GetById(SaleId id);

        Task Add(Sale sale);

        Task Update(Sale sale);

        Task Delete(Sale sale);

        Task<PagedResult<Sale>> GetAll(int page, int size, string? order);
    }
}
