using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public interface IProductRepository
    {
        Task<Product?> GetById(ProductId id);

        Task Add(Product product);

        Task Update(Product product);

        Task Delete(Product product);

        Task<PagedResult<Product>> GetAll(
            int page,
            int size,
            string? order);

        Task<PagedResult<Product>> GetByCategory(
            string category,
            int page,
            int size,
            string? order);

        Task<List<string>> GetCategories();
    }
}
