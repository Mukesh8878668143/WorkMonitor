using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs
{
    public class TimeENtryResponseDto
    {
        public int Id { get; set; }

        public DateOnly WorkDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public TimeEntryStatus Status { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
