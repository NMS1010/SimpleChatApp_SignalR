using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Backend.Infrastructure.Migrations
{
    public partial class EditMessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioRecord",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Messages",
                newName: "MessageType");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Messages",
                newName: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageType",
                table: "Messages",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "Messages",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "AudioRecord",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
