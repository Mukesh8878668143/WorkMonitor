using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Dasboard
{
    public class EmployerPerformanceDto
    {
        public int UserId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int HoldTasks { get; set; }

        public int BlockedTasks { get; set; }

        public int OverdueTasks { get; set; }

        public double CompletionPercentage { get; set; }
    }
}
