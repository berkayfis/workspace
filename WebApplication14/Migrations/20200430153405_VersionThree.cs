using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class VersionThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Form1",
                table: "Takvim",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Form1Toplanti",
                table: "Takvim",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Form1",
                table: "Takvim");

            migrationBuilder.DropColumn(
                name: "Form1Toplanti",
                table: "Takvim");
        }
    }
}
