using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OfficeWorkTracker.Domain.Constants;

namespace OfficeWorkTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var user = await _userService.CreateUserAsync(dto);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _userService.GetAllUserAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser( int id, UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, dto);
            if (updatedUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(updatedUser);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var delete = await _userService.DeleteUserAsync(id);
            if (!delete) { return NotFound(); }
            return NoContent();
        }
    }
}
