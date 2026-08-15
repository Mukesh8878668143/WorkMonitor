using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Interfaces;
using System.Security.Claims;

namespace OfficeWorkTracker.API.Controllers
{
    public class PauseTimeEntry : Controller
    {
        private readonly ITimeEntryService  _timeEntryService;

        public PauseTimeEntry(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }
        [HttpPost("pause")]
        public async Task<IActionResult> PauseTimeEntryAsync()
        {
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _timeEntryService.PauseTimeEntryAsync(userID);
            return Ok(result);
        }
    }
}
