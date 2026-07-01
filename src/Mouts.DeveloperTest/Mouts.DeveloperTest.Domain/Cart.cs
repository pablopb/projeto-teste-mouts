using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.Domain
{
    public sealed class Cart
    {
        public CartId CartId { get; private set; }

        public UserId UserId { get; private set; }

        public DateTime Date { get; private set; }

        private readonly List<CartItem> _items = new();

        public IReadOnlyCollection<CartItem> Products => _items;

        private Cart() { }

        public Cart(CartId cartId, UserId userId, DateTime date, List<CartItem> items)
        {
            CartId = cartId;
            UserId = userId;
            Date = date;
            _items = items ?? new();
        }
    }
}
