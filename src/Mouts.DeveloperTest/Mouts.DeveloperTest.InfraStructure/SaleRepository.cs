using Microsoft.EntityFrameworkCore;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.InfraStructure
{
    public sealed class SaleRepository(DataContext context) : ISaleRepository
    {
        public async Task<Sale?> GetById(SaleId id)
        {
            return await context.Sales
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.SaleId == id);
        }

        public async Task Add(Sale sale)
        {
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();
        }

        public async Task Update(Sale sale)
        {
            context.Sales.Update(sale);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Sale sale)
        {
            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
        }

        public async Task<PagedResult<Sale>> GetAll(int page, int size, string? order)
        {
            var query = context.Sales.Include(x => x.Items).AsQueryable();

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<Sale>
            {
                Data = data,
                CurrentPage = page,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling(total / (double)size)
            };
        }
    }
}
