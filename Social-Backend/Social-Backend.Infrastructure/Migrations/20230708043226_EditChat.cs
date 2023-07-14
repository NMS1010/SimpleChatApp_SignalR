using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Backend.Infrastructure.Migrations
{
    public partial class EditChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastMessage",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessageDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "LastMessage",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "LastMessageDate",
                table: "Chats");
        }
    }
}
