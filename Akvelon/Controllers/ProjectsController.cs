using Akvelon.Data;
using Akvelon.Models;
using Akvelon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace Akvelon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly ProjectsAPIDbContext dbContext;
        private readonly TasksAPIDbContext tasksDbContext;

        public ProjectsController(ProjectsAPIDbContext dbContext, TasksAPIDbContext tasksDbContext)
        {
            this.dbContext = dbContext;
            this.tasksDbContext = tasksDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await dbContext.Projects.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var project = await dbContext.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(AddProjectRequest addProjectRequest)
        { 
            var project = new Project()
            {
                Id = Guid.NewGuid(),
                Name = addProjectRequest.Name,
                StartDate = addProjectRequest.StartDate,
                CompletionDate = addProjectRequest.CompletionDate,
                Status = addProjectRequest.Status,
                Priority = addProjectRequest.Priority
            };

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            return Ok(project);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectRequest updateProjectRequest)
        {
            var project = await dbContext.Projects.FindAsync(id);

            if (project != null)
            {
                project.Name = updateProjectRequest.Name;
                project.StartDate = updateProjectRequest.StartDate;
                project.CompletionDate = updateProjectRequest.CompletionDate;
                project.Status = updateProjectRequest.Status;
                project.Priority = updateProjectRequest.Priority;

                await dbContext.SaveChangesAsync();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await dbContext.Projects.FindAsync(id);

            if (project != null)
            {
                // Checking for any Tasks in Project to delete them too
                if (project.TasksIds.Any())
                {
                    foreach (var i in ConverterService.StringToListId(project.TasksIds))
                    {
                        var task = await tasksDbContext.Tasks.FindAsync(i);
                        tasksDbContext.Remove(task);
                    }
                    tasksDbContext.SaveChanges();
                }
                dbContext.Remove(project);
                await dbContext.SaveChangesAsync();
                return Ok(project);
            }

            return NotFound();
        }
    }
}

