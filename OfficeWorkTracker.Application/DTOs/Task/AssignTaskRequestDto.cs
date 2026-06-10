using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.DTOs.Task
{
    public class AssignTaskRequestDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int AssignedToUserID { get; set; }

        public string Priority { get; set; }

        public DateTime? DueDate { get; set; } = DateTime.Now;
    }
}
