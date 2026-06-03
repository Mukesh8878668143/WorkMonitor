using OfficeWorkTracker.Application.DTOs.Dasboard;
using OfficeWorkTracker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Service
{
    public class Dashboards:IDashboardService
    {
        private readonly ITaskRespository _taskRespository;

        public Dashboards(ITaskRespository taskRespository)
        {
            _taskRespository = taskRespository;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int userid)
        {
            return new DashboardSummaryDto
            {
                TotalTasks = await _taskRespository.GetTotalTasksByUserAsync(userid),
                PendingTasks = await _taskRespository.GetPendingTaskByUserAsync(userid),
                InProgressTasks = await _taskRespository.GetInProgressTaskByUserAsync(userid),
                CompletedTasks = await _taskRespository.GetCompletedTaskByUserAsync(userid)
            };
        }

    }
}
