using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouts.DeveloperTest.Service;
using Mouts.DeveloperTest.Shared.Dtos.Product;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("products")]
    public sealed class ProductController(
     IProductService service)
     : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<PagedResult<ProductResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery(Name = "_page")] int page = 1,
            [FromQuery(Name = "_size")] int size = 10,
            [FromQuery(Name = "_order")] string? order = null)
        {
            return Ok(await service.GetAll(
                page,
                size,
                order));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType<ProductResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await service.GetById(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType<ProductResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            CreateProductRequest request)
        {
            var product = await service.Create(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType<ProductResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            int id,
            UpdateProductRequest request)
        {
            try
            {
                return Ok(await service.Update(id, request));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType<ProductResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await service.Delete(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("categories")]
        [ProducesResponseType<List<string>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await service.GetCategories());
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType<PagedResult<ProductResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategory(
            string category,
            [FromQuery(Name = "_page")] int page = 1,
            [FromQuery(Name = "_size")] int size = 10,
            [FromQuery(Name = "_order")] string? order = null)
        {
            return Ok(await service.GetByCategory(
                category,
                page,
                size,
                order));
        }
    }
}
