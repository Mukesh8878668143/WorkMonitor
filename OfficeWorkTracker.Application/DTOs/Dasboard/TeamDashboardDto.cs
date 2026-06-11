using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Dasboard
{
    public class TeamDashboardDto
    {

        public int TotalEmployees { get; set; }

        public int TotalTasks { get; set; }

        public int PendingTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int BlockedTasks { get; set; }
    }
}
