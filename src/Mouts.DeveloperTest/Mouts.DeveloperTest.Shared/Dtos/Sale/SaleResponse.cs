namespace Mouts.DeveloperTest.Shared.Dtos.Sale
{
    public sealed record SaleResponse
    {
        public int Id { get; init; }

        public string SaleNumber { get; init; } = string.Empty;

        public DateTime SaleDate { get; init; }

        public CustomerDto Customer { get; init; } = default!;

        public BranchDto Branch { get; init; } = default!;

        public List<SaleItemDto> Items { get; init; } = [];

        public bool Cancelled { get; init; }
    }

    public sealed record CustomerDto(int Id, string Name);

    public sealed record BranchDto(int Id, string Name);

    public sealed record SaleItemDto(
        int ProductId,
        string ProductTitle,
        int Quantity,
        decimal UnitPrice,
        decimal Discount,
        decimal Total,
        bool Cancelled);
}
