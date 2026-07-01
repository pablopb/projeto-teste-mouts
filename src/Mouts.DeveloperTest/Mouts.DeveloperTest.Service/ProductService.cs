using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.InfraStructure;
using Mouts.DeveloperTest.Shared.Dtos.Product;
using Mouts.DeveloperTest.Shared.Pagination;
using Mouts.DeveloperTest.Shared.Mappers;

namespace Mouts.DeveloperTest.Service
{
    public sealed class ProductService(
    IProductRepository repository)
    : IProductService
    {
        public async Task<ProductResponse> Create(CreateProductRequest request)
        {
            var product = new Product
            {
                ProductId = new ProductId(0),

                Title = request.Title,

                Price = request.Price,

                Description = request.Description,

                Category = request.Category,

                Image = request.Image,

                Rating = new Rating(
                    request.Rating.Rate,
                    request.Rating.Count)
            };

            await repository.Add(product);

            return product.ToResponse();
        }

        public async Task<ProductResponse?> GetById(int id)
        {
            var product = await repository.GetById(new ProductId(id));

            return product?.ToResponse();
        }

        public async Task<PagedResult<ProductResponse>> GetAll(
            int page,
            int size,
            string? order)
        {
            var result = await repository.GetAll(page, size, order);

            return new PagedResult<ProductResponse>
            {
                Data = result.Data
                    .Select(x => x.ToResponse())
                    .ToList(),

                CurrentPage = result.CurrentPage,

                TotalItems = result.TotalItems,

                TotalPages = result.TotalPages
            };
        }

        public async Task<PagedResult<ProductResponse>> GetByCategory(
            string category,
            int page,
            int size,
            string? order)
        {
            var result = await repository.GetByCategory(
                category,
                page,
                size,
                order);

            return new PagedResult<ProductResponse>
            {
                Data = result.Data
                    .Select(x => x.ToResponse())
                    .ToList(),

                CurrentPage = result.CurrentPage,

                TotalItems = result.TotalItems,

                TotalPages = result.TotalPages
            };
        }

        public async Task<ProductResponse> Update(
            int id,
            UpdateProductRequest request)
        {
            var product = await repository.GetById(new ProductId(id));

            if (product is null)
                throw new KeyNotFoundException();

            product = product with
            {
                Title = request.Title,

                Price = request.Price,

                Description = request.Description,

                Category = request.Category,

                Image = request.Image,

                Rating = new Rating(
                    request.Rating.Rate,
                    request.Rating.Count)
            };

            await repository.Update(product);

            return product.ToResponse();
        }

        public async Task<ProductResponse> Delete(int id)
        {
            var product = await repository.GetById(new ProductId(id));

            if (product is null)
                throw new KeyNotFoundException();

            await repository.Delete(product);

            return product.ToResponse();
        }

        public Task<List<string>> GetCategories()
        {
            return repository.GetCategories();
        }
    }
}
