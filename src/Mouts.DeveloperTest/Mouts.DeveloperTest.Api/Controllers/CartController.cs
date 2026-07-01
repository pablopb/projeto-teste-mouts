using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouts.DeveloperTest.Service;
using Mouts.DeveloperTest.Shared.Dtos.Cart;

namespace Mouts.DeveloperTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("carts")]
    public class CartController(ICartService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery(Name = "_page")] int page = 1,
            [FromQuery(Name = "_size")] int size = 10,
            [FromQuery(Name = "_order")] string? order = null)
        {
            return Ok(await service.GetAll(page, size, order));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cart = await service.GetById(id);

            if (cart is null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCartRequest request)
        {
            var cart = await service.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = cart.Id }, cart);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CreateCartRequest request)
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
    }
}
