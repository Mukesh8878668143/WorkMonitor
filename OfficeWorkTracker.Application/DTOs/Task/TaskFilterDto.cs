using OfficeWorkTracker.Application.DTOs.Commom;
using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Task
{
    public class TaskFilterDto:PaginationFilterDto
    {
        public string? Search { get; set; }

        public WorkTaskStatus? Status { get; set; }

        public TaskPriority? Priority { get; set; }

        public int? UserId { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
