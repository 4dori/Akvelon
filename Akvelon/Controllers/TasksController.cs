using Akvelon.Data;
using Akvelon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akvelon.Services;

namespace Akvelon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly TasksAPIDbContext dbContext;
        private readonly ProjectsAPIDbContext projectDbContext; //ProjectDB for the creation of dependencies with Tasks

        public TasksController(TasksAPIDbContext dbContext, ProjectsAPIDbContext projectDbContext)
        {
            this.dbContext = dbContext;
            this.projectDbContext = projectDbContext; //ProjectDB
        }



        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await dbContext.Tasks.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> AddTask(Guid id, AddTaskRequest addTaskRequest)
        {
            var project = await projectDbContext.Projects.FindAsync(id); // project instance to bind with a task

            if (project == null)
            {
                return NotFound();
            }

            var task = new Models.Task()
            {
                Id = Guid.NewGuid(),
                Name = addTaskRequest.Name,
                Status = addTaskRequest.Status,
                Description = addTaskRequest.Description,
                Priority = addTaskRequest.Priority,
                ProjectName = project.Name,
                ProjectId = project.Id
            };

            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();

            // Listed tasks and ids are converted to the string
            // If the task in project is not the first then we have to seperate task with ", " comma and space 
            if (project.TasksIds != "")
            {
                project.TasksIds += ", ";
            }
            if (project.Tasks != "")
            {
                project.Tasks += ", ";
            }
            project.TasksIds += task.Id;
            project.Tasks += task.Name;

            await projectDbContext.SaveChangesAsync();                      

            return Ok(task);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest updateTaskRequest)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            

            if (task != null)
            {
                task.Name = updateTaskRequest.Name;
                task.Status = updateTaskRequest.Status;
                task.Description = updateTaskRequest.Description;
                task.Priority = updateTaskRequest.Priority;

                await dbContext.SaveChangesAsync();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task != null)
            {
                var project = await projectDbContext.Projects.FindAsync(task.ProjectId);

                List<string> ProjectTasks = ConverterService.StringToList(project.Tasks);
                ProjectTasks.Remove(task.Name);

                List<Guid> ProjectTasksIds = ConverterService.StringToListId(project.TasksIds);
                ProjectTasksIds.Remove(task.Id);

                if (!ProjectTasks.Any())
                {
                    project.Tasks = "";
                    project.TasksIds = "";
                }
                else
                {
                    project.Tasks = ConverterService.ListToString(ProjectTasks);
                    project.TasksIds = ConverterService.ListIdToString(ProjectTasksIds);
                }   
                
                await projectDbContext.SaveChangesAsync();

                dbContext.Remove(task);
                await dbContext.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }
    }
}
