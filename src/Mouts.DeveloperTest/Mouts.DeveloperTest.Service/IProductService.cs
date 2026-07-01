using Mouts.DeveloperTest.Shared.Dtos.Product;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Service
{
    public interface IProductService
    {
        Task<ProductResponse> Create(CreateProductRequest request);

        Task<ProductResponse?> GetById(int id);

        Task<PagedResult<ProductResponse>> GetAll(
            int page,
            int size,
            string? order);

        Task<PagedResult<ProductResponse>> GetByCategory(
            string category,
            int page,
            int size,
            string? order);

        Task<ProductResponse> Update(
            int id,
            UpdateProductRequest request);

        Task<ProductResponse> Delete(int id);

        Task<List<string>> GetCategories();
    }
}
