using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class VersionOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgrenciOnayi",
                table: "OgrenciProjeOnerisi",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrenciOnayi",
                table: "OgrenciProjeOnerisi");
        }
    }
}
