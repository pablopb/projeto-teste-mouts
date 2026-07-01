using Mouts.DeveloperTest.Domain.Exceptions;

namespace Mouts.DeveloperTest.Domain
{
    public sealed class SaleItem
    {
        public int SaleId { get; private set; }

        public ProductReference Product { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        public decimal Total => (UnitPrice * Quantity) - Discount;

        public bool Cancelled { get; private set; }

        private SaleItem() { }

        public static SaleItem Create(
            int saleId,
            ProductReference product,
            int quantity,
            decimal unitPrice)
        {
            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");

            var discount = CalculateDiscount(quantity, unitPrice);

            return new SaleItem
            {
                SaleId = saleId,
                Product = product,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Discount = discount
            };
        }

        public void Cancel()
        {
            Cancelled = true;
        }

        private static decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            var subtotal = unitPrice * quantity;

            return quantity switch
            {
                >= 10 => subtotal * 0.20m,
                >= 4 => subtotal * 0.10m,
                _ => 0m
            };
        }
    }
}
