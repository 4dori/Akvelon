using Akvelon.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());
builder.Services.AddMvc();



builder.Services.AddDbContext<ProjectsAPIDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectsApiConnectionString")));
builder.Services.AddDbContext<TasksAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TasksApiConnectionString")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
