using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Dtos.Cart;

namespace Mouts.DeveloperTest.Shared.Mappers
{
    public static class CartMapper
    {
        public static CartResponse ToResponse(this Cart cart)
        {
            return new CartResponse
            {
                Id = cart.CartId.Value,
                UserId = cart.UserId.Value,
                Date = cart.Date,
                Products = cart.Products
                    .Select(x => new CartProductResponse
                    {
                        ProductId = x.ProductId.Value,
                        Quantity = x.Quantity
                    })
                    .ToList()
            };
        }
    }
}
