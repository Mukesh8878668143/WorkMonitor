using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Interfaces;
using System.Security.Claims;

namespace OfficeWorkTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTimeEntry()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token.");
            }
            int userId = int.Parse(userIdClaim.Value);
            var response = await _timeEntryService.StartTimeENtryAsync(userId);
            return Ok(response);
        }
    }
}
