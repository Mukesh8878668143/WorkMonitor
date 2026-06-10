using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Constants;
using System.Security.Claims;

namespace OfficeWorkTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize(Roles =$"{Roles.Admin},{Roles.Manager}")]
        [HttpGet]
        public async Task<IActionResult> GetDashboardSummary(int userId)
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync(userId);
            return Ok(summary);
        }
        
        [HttpGet("My")]
        public async Task<IActionResult> GetMyDashboard()
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIDClaim.Value);
            var summary = await _dashboardService.GetDashboardSummaryAsync(userId);
            return Ok(summary);
        }
    }
}
