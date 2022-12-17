using Akvelon.Models;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.Data
{
    public class ProjectsAPIDbContext : DbContext
    {
        public ProjectsAPIDbContext(DbContextOptions<ProjectsAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }


    }
}