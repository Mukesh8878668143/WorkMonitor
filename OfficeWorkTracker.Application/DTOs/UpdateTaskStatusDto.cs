using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTaskStatus = OfficeWorkTracker.Domain.Enum.WorkTaskStatus;

namespace OfficeWorkTracker.Application.DTOs
{
    public class UpdateTaskStatusDto
    {
        public WorkTaskStatus Status { get; set; } 
    }
}
