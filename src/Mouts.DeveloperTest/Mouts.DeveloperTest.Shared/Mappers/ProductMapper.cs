using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Shared.Dtos.Product;

namespace Mouts.DeveloperTest.Shared.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.ProductId.Value,

                Title = product.Title,

                Price = product.Price,

                Description = product.Description,

                Category = product.Category,

                Image = product.Image,

                Rating = new RatingResponse(
                    product.Rating.Rate,
                    product.Rating.Count)
            };
        }
    }
}
