using Microsoft.EntityFrameworkCore.Migrations;

namespace FindHelperApi.Migrations
{
    public partial class insertingPhotoOnPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Publications");
        }
    }
}
