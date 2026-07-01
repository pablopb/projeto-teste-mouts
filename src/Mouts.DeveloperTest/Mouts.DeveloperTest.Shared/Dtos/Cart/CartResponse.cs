namespace Mouts.DeveloperTest.Shared.Dtos.Cart
{
    public sealed record CartResponse
    {
        public int Id { get; init; }

        public int UserId { get; init; }

        public DateTime Date { get; init; }

        public List<CartProductResponse> Products { get; init; } = [];
    }

    public sealed record CartProductResponse
    {
        public int ProductId { get; init; }

        public int Quantity { get; init; }
    }
}
