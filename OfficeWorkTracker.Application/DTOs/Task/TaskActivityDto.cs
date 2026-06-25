using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Task
{
    public class TaskActivityDto
    {
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
