using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Entities
{
    public class TimeEntryBreak
    {
        public int Id { get; set; }

        public int TimeEntryId { get; set; }

        // Navigation Property
        public TimeEntry TimeEntry { get; set; } = null!;

        public DateTime BreakStartTime { get; set; }

        public DateTime? BreakEndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public string? Reason { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
