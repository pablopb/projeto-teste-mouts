namespace Mouts.DeveloperTest.Shared.Dtos.Cart
{
    public sealed record CreateCartRequest
    {
        public int UserId { get; init; }

        public DateTime Date { get; init; }

        public List<CreateCartProductRequest> Products { get; init; } = [];
    }

    public sealed record CreateCartProductRequest
    {
        public int ProductId { get; init; }

        public int Quantity { get; init; }
    }
}
