using Mouts.DeveloperTest.Shared.Dtos.Sale;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Service
{
    public interface ISaleService
    {
        Task<SaleResponse> Create(CreateSaleRequest request);

        Task<SaleResponse?> GetById(int id);

        Task<PagedResult<SaleResponse>> GetAll(int page, int size, string? order);

        Task<SaleResponse> Cancel(int id);
    }
}
