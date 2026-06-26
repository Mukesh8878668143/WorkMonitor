using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Dasboard
{
    public class TopPerformerDto
    {
        public int Rank { get; set; }

        public int UserId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }

        public int OverdueTasks { get; set; }

        public double CompletionPercentage { get; set; }
    }
}
