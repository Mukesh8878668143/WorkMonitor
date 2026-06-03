using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Interfaces;

namespace OfficeWorkTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDashboardSummary(int userId)
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync(userId);
            return Ok(summary);
        }
    }
}
