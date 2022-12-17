using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Akvelon.Migrations
{
    public partial class AddTasksNameAndID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tasks",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TasksIds",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TasksIds",
                table: "Projects");
        }
    }
}
