using Akvelon.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.Data
{
    public class TasksAPIDbContext : DbContext
    {
        public TasksAPIDbContext(DbContextOptions<TasksAPIDbContext> options) : base(options) 
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
    }
}
