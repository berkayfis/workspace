using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class versionFour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projeAdı = table.Column<string>(nullable: true),
                    projeTipi = table.Column<string>(nullable: true),
                    akademisyenID1 = table.Column<int>(nullable: false),
                    akademisyenID2 = table.Column<int>(nullable: false),
                    akademisyenID3 = table.Column<int>(nullable: false),
                    ogrNo1 = table.Column<string>(nullable: true),
                    ogrNo2 = table.Column<string>(nullable: true),
                    Seans = table.Column<string>(nullable: true),
                    Sinif = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finals", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finals");
        }
    }
}
