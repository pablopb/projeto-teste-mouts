using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.Domain
{
    public sealed record class Product
    {
        public ProductId ProductId { get; init; }

        public string Title { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public string Description { get; init; } = string.Empty;

        public string Category { get; init; } = string.Empty;

        public string Image { get; init; } = string.Empty;

        public Rating Rating { get; init; } = default!;
    }
}
