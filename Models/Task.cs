using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }

        //public string AssignedUserId { get; set; }
        //public User AssignedUser { get; set; }

        //public ICollection<Comment> Comments { get; set; }
    }

    public enum TaskStatus
    {
        Todo,
        InProgress,
        Done
    }

    public enum TaskPriority
    {
        High,
        Medium,
        Low
    }
}
