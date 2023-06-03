using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VailInstructorWikiApi.Migrations
{
    public partial class InstructorDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrailRating",
                table: "Run",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrailRating",
                table: "Run");
        }
    }
}
