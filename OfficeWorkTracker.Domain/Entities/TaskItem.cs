using OfficeWorkTracker.Domain.Enum;
namespace OfficeWorkTracker.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public Enum.TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int UserId { get; set; }


        // Navigation Property
        public User User { get; set; }

        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    }
}
