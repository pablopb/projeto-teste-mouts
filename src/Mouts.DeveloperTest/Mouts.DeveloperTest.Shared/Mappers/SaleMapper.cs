using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Dtos.Sale;

namespace Mouts.DeveloperTest.Shared.Mappers
{
    public static class SaleMapper
    {
        public static SaleResponse ToResponse(this Sale sale)
        {
            return new SaleResponse
            {
                Id = sale.SaleId.Value,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Cancelled = sale.Cancelled,

                Customer = new CustomerDto(
                    sale.Customer.CustomerId,
                    sale.Customer.CustomerName),

                Branch = new BranchDto(
                    sale.Branch.BranchId,
                    sale.Branch.BranchName),

                Items = sale.Items.Select(x => new SaleItemDto(
                    x.Product.ProductId,
                    x.Product.ProductTitle,
                    x.Quantity,
                    x.UnitPrice,
                    x.Discount,
                    x.Total,
                    x.Cancelled
                )).ToList()
            };
        }
    }
}
