
namespace Mouts.DeveloperTest.Shared.Dtos.Sale
{
    public sealed record CreateSaleRequest
    {
        public int CustomerId { get; init; }

        public string CustomerName { get; init; } = string.Empty;

        public int BranchId { get; init; }

        public string BranchName { get; init; } = string.Empty;

        public DateTime SaleDate { get; init; }

        public List<CreateSaleItemRequest> Items { get; init; } = [];
    }

    public sealed record CreateSaleItemRequest
    {
        public int ProductId { get; init; }

        public string ProductTitle { get; init; } = string.Empty;

        public int Quantity { get; init; }

        public decimal UnitPrice { get; init; }
    }
}
