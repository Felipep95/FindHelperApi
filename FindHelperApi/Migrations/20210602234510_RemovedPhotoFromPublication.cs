using Microsoft.EntityFrameworkCore.Migrations;

namespace FindHelperApi.Migrations
{
    public partial class RemovedPhotoFromPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Publications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
