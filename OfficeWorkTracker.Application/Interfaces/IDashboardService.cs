using OfficeWorkTracker.Application.DTOs.Dasboard;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(int userId);

        Task<TeamDashboardDto> GetTeamDashboardAsync();

        Task<List<EmployerPerformanceDto>> GetEmployerPerformanceAsync();

        Task<List<TopPerformerDto>> GetTopPerformersAsync();
    }
}
