namespace Mouts.DeveloperTest.Shared.Dtos.Product
{
    public sealed record UpdateProductRequest
    {
        public string Title { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public string Description { get; init; } = string.Empty;

        public string Category { get; init; } = string.Empty;

        public string Image { get; init; } = string.Empty;

        public RatingRequest Rating { get; init; } = default!;
    }
}
