using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouts.DeveloperTest.Service;
using Mouts.DeveloperTest.Shared.Dtos.Sale;

namespace Mouts.DeveloperTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("sales")]
    public sealed class SaleController(ISaleService service) : ControllerBase
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
            var sale = await service.GetById(id);

            if (sale is null)
                return NotFound();

            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleRequest request)
        {
            var sale = await service.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }

        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                return Ok(await service.Cancel(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
