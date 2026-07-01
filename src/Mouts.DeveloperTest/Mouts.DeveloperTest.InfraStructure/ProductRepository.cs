using Microsoft.EntityFrameworkCore;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;
using System.Linq.Expressions;
namespace Mouts.DeveloperTest.InfraStructure
{
    public sealed class ProductRepository(DataContext context) : IProductRepository
    {
        public async Task<Product?> GetById(ProductId id)
        {
            return await context.Products
                .FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task Add(Product product)
        {
            await context.Products.AddAsync(product);

            await context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            context.Products.Update(product);

            await context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }

        public async Task<PagedResult<Product>> GetAll(
            int page,
            int size,
            string? order)
        {
            IQueryable<Product> query = context.Products;

            query = ApplyOrdering(query, order);

            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Data = products,
                CurrentPage = page,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        public async Task<PagedResult<Product>> GetByCategory(
            string category,
            int page,
            int size,
            string? order)
        {
            IQueryable<Product> query = context.Products
                .Where(x => x.Category == category);

            query = ApplyOrdering(query, order);

            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Data = products,
                CurrentPage = page,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        public async Task<List<string>> GetCategories()
        {
            return await context.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
        }

        private static IQueryable<Product> ApplyOrdering(
            IQueryable<Product> query,
            string? order)
        {
            if (string.IsNullOrWhiteSpace(order))
                return query.OrderBy(x => x.Title);

            IOrderedQueryable<Product>? ordered = null;

            foreach (var item in order.Split(','))
            {
                var parts = item.Trim().Split(' ');

                var property = parts[0].ToLower();

                var desc = parts.Length > 1 &&
                           parts[1].Equals(
                               "desc",
                               StringComparison.OrdinalIgnoreCase);

                ordered = property switch
                {
                    "title" => Apply(
                        ordered,
                        query,
                        x => x.Title,
                        desc),

                    "price" => Apply(
                        ordered,
                        query,
                        x => x.Price,
                        desc),

                    "category" => Apply(
                        ordered,
                        query,
                        x => x.Category,
                        desc),

                    _ => ordered
                };
            }

            return ordered ?? query;
        }

        private static IOrderedQueryable<Product> Apply<TKey>(
            IOrderedQueryable<Product>? ordered,
            IQueryable<Product> query,
            Expression<Func<Product, TKey>> selector,
            bool desc)
        {
            if (ordered is null)
            {
                return desc
                    ? query.OrderByDescending(selector)
                    : query.OrderBy(selector);
            }

            return desc
                ? ordered.ThenByDescending(selector)
                : ordered.ThenBy(selector);
        }
    }
}
