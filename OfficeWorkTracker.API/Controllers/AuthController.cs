using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.DTOs.Auth;
using OfficeWorkTracker.Application.Interfaces;

namespace OfficeWorkTracker.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await userService.LoginAsync(dto);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
