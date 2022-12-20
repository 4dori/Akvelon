using Akvelon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Akvelon", 
        Version = "v1", 
        Description = "An API to perform Project operations",
        Contact = new OpenApiContact
        {
            Name = "Arslan Mukhamatnurov mail",
            Email = "mukhamatnurov13@gmail.com"
        }
    });
});

builder.Services.AddDbContext<ProjectsAPIDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectsApiConnectionString")));
builder.Services.AddDbContext<TasksAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TasksApiConnectionString")));
var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
