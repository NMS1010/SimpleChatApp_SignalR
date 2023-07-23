using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Backend.Infrastructure.Migrations
{
    public partial class AddMessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioRecord",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioRecord",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Messages");
        }
    }
}
