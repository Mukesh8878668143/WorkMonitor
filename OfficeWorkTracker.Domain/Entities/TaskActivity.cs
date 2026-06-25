using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Entities
{
    public class TaskActivity
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public ActivityType ActivityType { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public TaskItem Task { get; set; }

        public User User { get; set; }
    }
}
