using Microsoft.EntityFrameworkCore;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public sealed class CartRepository(DataContext context) : ICartRepository
    {
        public async Task<Cart?> GetById(CartId id)
        {
            return await context.Carts
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.CartId == id);
        }

        public async Task Add(Cart cart)
        {
            await context.Carts.AddAsync(cart);
            await context.SaveChangesAsync();
        }

        public async Task Update(Cart cart)
        {
            context.Carts.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Cart cart)
        {
            context.Carts.Remove(cart);
            await context.SaveChangesAsync();
        }

        public async Task<PagedResult<Cart>> GetAll(int page, int size, string? order)
        {
            var query = context.Carts.Include(x => x.Products).AsQueryable();

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Cart>
            {
                Data = data,
                TotalItems = total,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)size)
            };
        }
    }
}
