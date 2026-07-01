using Mouts.DeveloperTest.Domain.DomainEvents;
using Mouts.DeveloperTest.Domain.Exceptions;
using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.Domain
{
    public sealed class Sale
    {
        public SaleId SaleId { get; private set; }

        public string SaleNumber { get; private set; }

        public DateTime SaleDate { get; private set; }

        public CustomerReference Customer { get; private set; }

        public BranchReference Branch { get; private set; }

        private readonly List<SaleItem> _items = new();

        public IReadOnlyCollection<SaleItem> Items => _items;

        public bool Cancelled { get; private set; }

        private readonly List<IDomainEvent> _events = new();

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        private Sale() { }

        #region Factory

        public static Sale Create(
            SaleId id,
            string saleNumber,
            CustomerReference customer,
            BranchReference branch,
            DateTime date)
        {
            var sale = new Sale
            {
                SaleId = id,
                SaleNumber = saleNumber,
                Customer = customer,
                Branch = branch,
                SaleDate = date,
                Cancelled = false
            };

            sale.Raise(new SaleCreatedEvent(id.Value));

            return sale;
        }

        #endregion

        #region Behavior

        public void AddItem(ProductReference product, int quantity, decimal unitPrice)
        {
            var item = SaleItem.Create(SaleId.Value,product, quantity, unitPrice);

            _items.Add(item);
        }

        public void Cancel()
        {
            if (Cancelled)
                return;

            Cancelled = true;

            foreach (var item in _items)
                item.Cancel();

            Raise(new SaleCancelledEvent(SaleId.Value));
        }

        public void CancelItem(int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.ProductId == productId);

            if (item is null)
                throw new DomainException("Item not found");

            item.Cancel();

            Raise(new ItemCancelledEvent(SaleId.Value, productId));
        }

        #endregion

        #region Events

        private void Raise(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        #endregion
    }
}
