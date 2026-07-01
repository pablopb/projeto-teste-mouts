using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouts.DeveloperTest.Api.DTOs;
using Mouts.DeveloperTest.Service;

namespace Mouts.DeveloperTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : Controller
    {

        [AllowAnonymous]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var token = await authService.Authenticate(userName, password);
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();
            return Ok(new AuthDto(token));
        }
    }
}
