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

        public async Task<List<EmployerPerformanceDto>> GetEmployerPerformanceAsync()
        {
            var tasks =
         await _taskRepository
             .GetAllTaskWithUserAsync();

            var result = tasks
                .GroupBy(x => new
                {
                    x.UserId,
                    x.User.FullName
                })
                .Select(g => new EmployerPerformanceDto
                {
                    UserId = g.Key.UserId,

                    EmployeeName =
                        g.Key.FullName,

                    TotalTasks = g.Count(),

                    CompletedTasks =
                        g.Count(x =>
                            x.Status ==
                            WorkTaskStatus.Completed),

                    PendingTasks =
                        g.Count(x =>
                            x.Status ==
                            WorkTaskStatus.Pending),

                    InProgressTasks =
                        g.Count(x =>
                            x.Status ==
                            WorkTaskStatus.InProgress),

                    BlockedTasks =
                        g.Count(x =>
                            x.Status ==
                            WorkTaskStatus.Blocked),

                    HoldTasks = 
                    g.Count(x =>
                        x.Status ==
                        WorkTaskStatus.Hold),

                    OverdueTasks = 
                    g.Count(x 
                        => x.DueDate.HasValue &&
                        x.DueDate.Value.Date < DateTime.UtcNow.Date &&
                        x.Status != WorkTaskStatus.Completed),

                    CompletionPercentage =
                        g.Count() == 0
                        ? 0
                        : Math.Round(
                            (double)
                            g.Count(x =>
                                x.Status ==
                                WorkTaskStatus.Completed)
                            / g.Count() * 100,
                            2)
                })
                .OrderByDescending(
                    x => x.CompletionPercentage)
                .ToList();

            return result;
        }

        public async Task<List<TopPerformerDto>> GetTopPerformersAsync()
        {
            var performance = await GetEmployerPerformanceAsync();
            var ranking = performance
                           .OrderByDescending(x=>x.CompletionPercentage)
                           .ThenByDescending(x=>x.CompletedTasks)
                           .ThenBy(x=>x.OverdueTasks)
                           .ToList();

            var result = ranking.Select((employess,Index)=> new TopPerformerDto { 
                Rank = Index+1,
                UserId = employess.UserId,
                EmployeeName = employess.EmployeeName,
                TotalTasks = employess.TotalTasks,
                CompletedTasks = employess.CompletedTasks,
                PendingTasks = employess.PendingTasks,
                OverdueTasks = employess.OverdueTasks,
                CompletionPercentage = employess.CompletionPercentage
            }).ToList();
            return result;
        }
    }
}
