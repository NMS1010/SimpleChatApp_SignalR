using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Backend.Infrastructure.Migrations
{
    public partial class AddChatRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatRoleId",
                table: "UserChats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChatRole",
                columns: table => new
                {
                    ChatRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRole", x => x.ChatRoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatRoleId",
                table: "UserChats",
                column: "ChatRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_ChatRole_ChatRoleId",
                table: "UserChats",
                column: "ChatRoleId",
                principalTable: "ChatRole",
                principalColumn: "ChatRoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_ChatRole_ChatRoleId",
                table: "UserChats");

            migrationBuilder.DropTable(
                name: "ChatRole");

            migrationBuilder.DropIndex(
                name: "IX_UserChats_ChatRoleId",
                table: "UserChats");

            migrationBuilder.DropColumn(
                name: "ChatRoleId",
                table: "UserChats");
        }
    }
}
