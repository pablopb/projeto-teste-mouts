using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;
using Mouts.DeveloperTest.InfraStructure;
using Mouts.DeveloperTest.Shared.Dtos.Cart;
using Mouts.DeveloperTest.Shared.Pagination;
using Mouts.DeveloperTest.Shared.Mappers;

namespace Mouts.DeveloperTest.Service
{
    public sealed class CartService(ICartRepository repository) : ICartService
    {
        public async Task<CartResponse> Create(CreateCartRequest request)
        {
            var cart = new Cart(new CartId(0),
                new UserId(request.UserId),
                request.Date,
                request.Products
                    .Select(x => new CartItem(
                        new ProductId(x.ProductId),
                        x.Quantity))
                    .ToList());

            await repository.Add(cart);

            return cart.ToResponse();
        }

        public async Task<CartResponse?> GetById(int id)
        {
            var cart = await repository.GetById(new CartId(id));

            return cart?.ToResponse();
        }

        public async Task<PagedResult<CartResponse>> GetAll(int page, int size, string? order)
        {
            var result = await repository.GetAll(page, size, order);

            return new PagedResult<CartResponse>
            {
                Data = result.Data.Select(x => x.ToResponse()).ToList(),
                CurrentPage = result.CurrentPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public async Task<CartResponse> Update(int id, CreateCartRequest request)
        {
            var cart = await repository.GetById(new CartId(id));

            if (cart is null)
                throw new KeyNotFoundException();

            var updated = new Cart(
                cart.CartId,
                new UserId(request.UserId),
                request.Date,
                request.Products
                    .Select(x => new CartItem(
                        new ProductId(x.ProductId),
                        x.Quantity))
                    .ToList());

            await repository.Update(updated);

            return updated.ToResponse();
        }

        public async Task<CartResponse> Delete(int id)
        {
            var cart = await repository.GetById(new CartId(id));

            if (cart is null)
                throw new KeyNotFoundException();

            await repository.Delete(cart);

            return cart.ToResponse();
        }
    }
}
