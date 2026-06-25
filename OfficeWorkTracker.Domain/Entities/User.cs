using OfficeWorkTracker.Domain.Constants;
using System;
using System.Security.Policy;

namespace OfficeWorkTracker.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = Roles.User;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();

        public ICollection<TaskActivity> Activities { get; set; } = new List<TaskActivity>();
    }
}
