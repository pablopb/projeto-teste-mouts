using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.InfraStructure;
using Mouts.DeveloperTest.Shared.Dtos.Sale;
using Mouts.DeveloperTest.Shared.Pagination;
using Mouts.DeveloperTest.Shared.Mappers;
using Mouts.DeveloperTest.Service.EventNotification;

namespace Mouts.DeveloperTest.Service
{
    public sealed class SaleService(ISaleRepository repository, IEventPublisher publisher) : ISaleService
    {
        public async Task<SaleResponse> Create(CreateSaleRequest request)
        {
            var sale = Sale.Create(
                new SaleId(0),
                $"SALE-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                new CustomerReference(request.CustomerId, request.CustomerName),
                new BranchReference(request.BranchId, request.BranchName),
                request.SaleDate);

            foreach (var item in request.Items)
            {
                sale.AddItem(
                    new ProductReference(item.ProductId, item.ProductTitle),
                    item.Quantity,
                    item.UnitPrice);
            }

            await repository.Add(sale);

            await publisher.PublishAsync(sale.Events);

            sale.ClearEvents();

            return sale.ToResponse();
        }

        public async Task<SaleResponse?> GetById(int id)
        {
            var sale = await repository.GetById(new SaleId(id));

            return sale?.ToResponse();
        }

        public async Task<PagedResult<SaleResponse>> GetAll(int page, int size, string? order)
        {
            var result = await repository.GetAll(page, size, order);

            return new PagedResult<SaleResponse>
            {
                Data = result.Data.Select(x => x.ToResponse()).ToList(),
                CurrentPage = result.CurrentPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public async Task<SaleResponse> Cancel(int id)
        {
            var sale = await repository.GetById(new SaleId(id));

            if (sale is null)
                throw new KeyNotFoundException();

            sale.Cancel();

            await repository.Update(sale);

            await publisher.PublishAsync(sale.Events);

            sale.ClearEvents();

            return sale.ToResponse();
        }
    }
}
