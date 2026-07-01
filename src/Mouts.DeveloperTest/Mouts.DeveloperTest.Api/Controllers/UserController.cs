using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouts.DeveloperTest.Service;
using Mouts.DeveloperTest.Shared.Dtos.User;
using Mouts.DeveloperTest.Shared.Pagination;

namespace Mouts.DeveloperTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public sealed class UserController(IUserService service) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<CreateUserResponse>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var response = await service.Create(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetById(id);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType<PagedResult<UserResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
    [FromQuery(Name = "_page")] int page = 1,
    [FromQuery(Name = "_size")] int size = 10,
    [FromQuery(Name = "_order")] string? order = null)
        {
            var response = await service.GetAll(
                page,
                size,
                order);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
    int id,
    UpdateUserRequest request)
        {
            try
            {
                var response = await service.Update(id, request);

                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await service.Delete(id);

                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
