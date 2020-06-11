using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class versionFive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SinavTarihi",
                table: "Finals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "akademisyenKisaltma1",
                table: "Finals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "akademisyenKisaltma2",
                table: "Finals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "akademisyenKisaltma3",
                table: "Finals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavTarihi",
                table: "Finals");

            migrationBuilder.DropColumn(
                name: "akademisyenKisaltma1",
                table: "Finals");

            migrationBuilder.DropColumn(
                name: "akademisyenKisaltma2",
                table: "Finals");

            migrationBuilder.DropColumn(
                name: "akademisyenKisaltma3",
                table: "Finals");
        }
    }
}
