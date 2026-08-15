using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Entities
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateOnly WorkDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public TimeEntryStatus Status { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsManualEntry { get; set; } = false;
        public ICollection<TimeEntryBreak> Breaks { get; set; } = new List<TimeEntryBreak>();


    }
}
