using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VailInstructorWikiApi.Migrations
{
    public partial class InstructorDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Displine",
                table: "Skill",
                newName: "Discpline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discpline",
                table: "Skill",
                newName: "Displine");
        }
    }
}
