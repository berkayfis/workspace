using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class VersionTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProjeOnerileri",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjeOnerileri");
        }
    }
}
