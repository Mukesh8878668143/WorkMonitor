using OfficeWorkTracker.Application.DTOs.Dasboard;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTaskStatus = OfficeWorkTracker.Domain.Enum.WorkTaskStatus;

namespace OfficeWorkTracker.Application.Service
{
    public class Dashboards : IDashboardService
    {
        private readonly ITaskRespository _taskRepository;
        private readonly IUserRepository _userRepository;
        public Dashboards(ITaskRespository taskRespository, IUserRepository userRepository)
        {
            _taskRepository = taskRespository;
            _userRepository = userRepository;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int userid)
        {
            return new DashboardSummaryDto
            {
                TotalTasks = await _taskRepository.GetTotalTasksByUserAsync(userid),
                PendingTasks = await _taskRepository.GetPendingTaskByUserAsync(userid),
                InProgressTasks = await _taskRepository.GetInProgressTaskByUserAsync(userid),
                CompletedTasks = await _taskRepository.GetCompletedTaskByUserAsync(userid)
            };
        }
        public async Task<TeamDashboardDto> GetTeamDashboardAsync()
        {
            return new TeamDashboardDto
            {
                TotalEmployees = await _userRepository.GetTotalUserCountAsync(),
                TotalTasks = await _taskRepository.GetTotalTaskCountAsync(),
                PendingTasks = await _taskRepository.GetTaskCountByStatusAsync(Domain.Enum.WorkTaskStatus.Pending),
                InProgressTasks =
            await _taskRepository
                .GetTaskCountByStatusAsync(
                    WorkTaskStatus.InProgress),

                CompletedTasks =
            await _taskRepository
                .GetTaskCountByStatusAsync(
                    WorkTaskStatus.Completed),

                BlockedTasks =
                await _taskRepository
                    .GetTaskCountByStatusAsync(
                        WorkTaskStatus.Blocked)
            };
        }
    }
}
