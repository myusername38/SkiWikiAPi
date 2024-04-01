using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VailInstructorWikiApi.Migrations
{
    public partial class InstructorDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Level",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Discpline",
                table: "Level",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelNumber",
                table: "Level",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Level",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Discipline",
                table: "Drill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Drill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Drill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "Discpline",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "LevelNumber",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "Drill");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Drill");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Drill");
        }
    }
}
