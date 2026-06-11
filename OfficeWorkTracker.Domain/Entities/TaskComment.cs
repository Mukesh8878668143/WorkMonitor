using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Entities
{
    public class TaskComment
    {
        /// <summary>
        /// To track the comment history of the task, we can create a TaskComment entity that stores the comment text, the user who made the comment, and the timestamp of when the comment was made. This way, we can maintain a record of all comments associated with a task and provide context for any updates or changes made to the task.
        /// </summary>
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
