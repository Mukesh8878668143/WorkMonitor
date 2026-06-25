using OfficeWorkTracker.Domain.Enum;
using WorkTaskStatus = OfficeWorkTracker.Domain.Enum.WorkTaskStatus;

namespace OfficeWorkTracker.Application.DTOs.Task
{
    public class TaskResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public WorkTaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int UserId { get; set; }
    }
}
