namespace Mouts.DeveloperTest.Shared.Dtos.Product
{
    public sealed record ProductResponse
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public string Description { get; init; } = string.Empty;

        public string Category { get; init; } = string.Empty;

        public string Image { get; init; } = string.Empty;

        public RatingResponse Rating { get; init; } = default!;
    }

    public sealed record RatingResponse(
        decimal Rate,
        int Count);
}
