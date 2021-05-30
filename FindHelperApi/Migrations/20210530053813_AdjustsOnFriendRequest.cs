using Microsoft.EntityFrameworkCore.Migrations;

namespace FindHelperApi.Migrations
{
    public partial class AdjustsOnFriendRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FriendRequests");

            migrationBuilder.AddColumn<bool>(
                name: "isFriend",
                table: "FriendRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFriend",
                table: "FriendRequests");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FriendRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
