using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public int UserId { get; set; }
    }
}
