using Mouts.DeveloperTest.Domain.Exceptions;

namespace Mouts.DeveloperTest.Domain
{
    public static class SaleDiscountPolicy
    {
        public static decimal Calculate(int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");

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
