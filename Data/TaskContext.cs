using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {

        }
        public DbSet<TaskManagement.Models.Task> Tasks { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TaskManagement.Models.Task>().ToTable("Tasks");

        }
    }
}
