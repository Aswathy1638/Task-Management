namespace TaskManagement.Models
{
    public class User
    {
        public string FullName { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        //public ICollection<Notification> Notifications { get; set; }
    }
}
