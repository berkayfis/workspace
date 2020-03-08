using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication14.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkademikPersonel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    Anabilimdali = table.Column<string>(nullable: true),
                    Unvan = table.Column<string>(nullable: true),
                    Kisaltma = table.Column<string>(nullable: true),
                    KullaniciAdi = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademikPersonel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlanOturum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlanOturum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Belgeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Isim = table.Column<string>(nullable: true),
                    ErisimYolu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Belgeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenci",
                columns: table => new
                {
                    OgrenciNo = table.Column<string>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenci", x => x.OgrenciNo);
                });

            migrationBuilder.CreateTable(
                name: "Takvim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Form2 = table.Column<DateTime>(nullable: true),
                    Toplanti = table.Column<DateTime>(nullable: true),
                    Ret = table.Column<DateTime>(nullable: true),
                    Ararapor1 = table.Column<DateTime>(nullable: true),
                    Ararapor2 = table.Column<DateTime>(nullable: true),
                    Finalrapor = table.Column<DateTime>(nullable: true),
                    Arasinav = table.Column<DateTime>(nullable: true),
                    Bitirmesinav = table.Column<DateTime>(nullable: true),
                    Kitap = table.Column<DateTime>(nullable: true),
                    Bütünlemerapor = table.Column<DateTime>(nullable: true),
                    Arabütünleme = table.Column<DateTime>(nullable: true),
                    Bitirmebutunleme = table.Column<DateTime>(nullable: true),
                    Bkitap = table.Column<DateTime>(nullable: true),
                    Donem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takvim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjeKoordinatoru",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademisyenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjeKoordinatoru", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PROJE_KOO__Akade__5CD6CB2B",
                        column: x => x.AkademisyenId,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EskiKabulGorenProjeler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanismanId = table.Column<int>(nullable: true),
                    Isim = table.Column<string>(nullable: true),
                    OturumNo = table.Column<int>(nullable: true),
                    KisiSayisi = table.Column<int>(nullable: true),
                    GrupSayisi = table.Column<int>(nullable: true),
                    Turu = table.Column<string>(nullable: true),
                    Form1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EskiKabulGorenProjeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ESKI_KABU__DANIS__4222D4EF",
                        column: x => x.DanismanId,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ESKI_KABU__OTURU__4316F928",
                        column: x => x.OturumNo,
                        principalTable: "AlanOturum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjeOnerileri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanismanId = table.Column<int>(nullable: true),
                    Isim = table.Column<string>(nullable: true),
                    OturumNo = table.Column<int>(nullable: true),
                    KisiSayisi = table.Column<int>(nullable: true),
                    GrupSayisi = table.Column<int>(nullable: true),
                    Kategori = table.Column<string>(nullable: true),
                    Form1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjeOnerileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PROJE_ONE__DANIS__145C0A3F",
                        column: x => x.DanismanId,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PROJE_ONE__OTURU__15502E78",
                        column: x => x.OturumNo,
                        principalTable: "AlanOturum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EskiBasarisizAlinanProje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ogrno1 = table.Column<string>(nullable: true),
                    Ogrno2 = table.Column<string>(nullable: true),
                    Danismanid = table.Column<int>(nullable: true),
                    Isim = table.Column<string>(nullable: true),
                    OturumNo = table.Column<int>(nullable: true),
                    Turu = table.Column<string>(nullable: true),
                    Form2 = table.Column<string>(nullable: true),
                    Statu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EskiBasarisizAlinanProje", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ESKI_BASA__DANIS__47DBAE45",
                        column: x => x.Danismanid,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ESKI_BASA__OGRNO__45F365D3",
                        column: x => x.Ogrno1,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ESKI_BASA__OGRNO__46E78A0C",
                        column: x => x.Ogrno2,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ESKI_BASA__OTURU__48CFD27E",
                        column: x => x.OturumNo,
                        principalTable: "AlanOturum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciProjeOnerisi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ogrno1 = table.Column<string>(nullable: true),
                    Ogrno2 = table.Column<string>(nullable: true),
                    Danismanid = table.Column<int>(nullable: true),
                    Isim = table.Column<string>(nullable: true),
                    OturumNo = table.Column<int>(nullable: true),
                    Turu = table.Column<string>(nullable: true),
                    Form2 = table.Column<string>(nullable: true),
                    Statu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciProjeOnerisi", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OGRENCI_P__DANIS__31EC6D26",
                        column: x => x.Danismanid,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OGRENCI_P__OGRNO__33D4B598",
                        column: x => x.Ogrno1,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OGRENCI_P__OGRNO__34C8D9D1",
                        column: x => x.Ogrno2,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OGRENCI_P__OTURU__32E0915F",
                        column: x => x.OturumNo,
                        principalTable: "AlanOturum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Duyuru",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(nullable: true),
                    İcerik = table.Column<string>(nullable: true),
                    Eklenti = table.Column<string>(nullable: true),
                    Zaman = table.Column<DateTime>(nullable: true),
                    KoordinatorNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyuru", x => x.Id);
                    table.ForeignKey(
                        name: "FK__DUYURU__Koordina__5FB337D6",
                        column: x => x.KoordinatorNo,
                        principalTable: "ProjeKoordinatoru",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Istek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjeId = table.Column<int>(nullable: true),
                    OgrNo1 = table.Column<string>(nullable: true),
                    OgrNo2 = table.Column<string>(nullable: true),
                    Form2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Istek", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ISTEK__OgrNo1__2E1BDC42",
                        column: x => x.OgrNo1,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ISTEK__OgrNo2__2F10007B",
                        column: x => x.OgrNo2,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ISTEK__ProjeID__2D27B809",
                        column: x => x.ProjeId,
                        principalTable: "ProjeOnerileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjeAl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjeNo = table.Column<int>(nullable: true),
                    OgrNo1 = table.Column<string>(nullable: true),
                    OgrNo2 = table.Column<string>(nullable: true),
                    Form2 = table.Column<string>(nullable: true),
                    ProjeDurumu = table.Column<string>(nullable: true),
                    KabulDurumu = table.Column<string>(nullable: true),
                    KurulAciklama = table.Column<string>(nullable: true),
                    OgrenciOneriNo = table.Column<int>(nullable: true),
                    Ararapor1 = table.Column<string>(nullable: true),
                    Ararapor2 = table.Column<string>(nullable: true),
                    Finalrapor = table.Column<string>(nullable: true),
                    Finalkitap = table.Column<string>(nullable: true),
                    Butunlemerapor = table.Column<string>(nullable: true),
                    Butunlemekitap = table.Column<string>(nullable: true),
                    AsistanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjeAl", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PROJE_AL__ASISTA__1AD3FDA4",
                        column: x => x.AsistanId,
                        principalTable: "AkademikPersonel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PROJE_AL__OGR_NO__29572725",
                        column: x => x.OgrNo1,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PROJE_AL__OGR_NO__2A4B4B5E",
                        column: x => x.OgrNo2,
                        principalTable: "Ogrenci",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PROJE_AL__OGRENC__35BCFE0A",
                        column: x => x.OgrenciOneriNo,
                        principalTable: "OgrenciProjeOnerisi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PROJE_AL__PROJE___286302EC",
                        column: x => x.ProjeNo,
                        principalTable: "ProjeOnerileri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Duyuru_KoordinatorNo",
                table: "Duyuru",
                column: "KoordinatorNo");

            migrationBuilder.CreateIndex(
                name: "IX_EskiBasarisizAlinanProje_Danismanid",
                table: "EskiBasarisizAlinanProje",
                column: "Danismanid");

            migrationBuilder.CreateIndex(
                name: "IX_EskiBasarisizAlinanProje_Ogrno1",
                table: "EskiBasarisizAlinanProje",
                column: "Ogrno1");

            migrationBuilder.CreateIndex(
                name: "IX_EskiBasarisizAlinanProje_Ogrno2",
                table: "EskiBasarisizAlinanProje",
                column: "Ogrno2");

            migrationBuilder.CreateIndex(
                name: "IX_EskiBasarisizAlinanProje_OturumNo",
                table: "EskiBasarisizAlinanProje",
                column: "OturumNo");

            migrationBuilder.CreateIndex(
                name: "IX_EskiKabulGorenProjeler_DanismanId",
                table: "EskiKabulGorenProjeler",
                column: "DanismanId");

            migrationBuilder.CreateIndex(
                name: "IX_EskiKabulGorenProjeler_OturumNo",
                table: "EskiKabulGorenProjeler",
                column: "OturumNo");

            migrationBuilder.CreateIndex(
                name: "IX_Istek_OgrNo1",
                table: "Istek",
                column: "OgrNo1");

            migrationBuilder.CreateIndex(
                name: "IX_Istek_OgrNo2",
                table: "Istek",
                column: "OgrNo2");

            migrationBuilder.CreateIndex(
                name: "IX_Istek_ProjeId",
                table: "Istek",
                column: "ProjeId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProjeOnerisi_Danismanid",
                table: "OgrenciProjeOnerisi",
                column: "Danismanid");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProjeOnerisi_Ogrno1",
                table: "OgrenciProjeOnerisi",
                column: "Ogrno1");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProjeOnerisi_Ogrno2",
                table: "OgrenciProjeOnerisi",
                column: "Ogrno2");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProjeOnerisi_OturumNo",
                table: "OgrenciProjeOnerisi",
                column: "OturumNo");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeAl_AsistanId",
                table: "ProjeAl",
                column: "AsistanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeAl_OgrNo1",
                table: "ProjeAl",
                column: "OgrNo1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeAl_OgrNo2",
                table: "ProjeAl",
                column: "OgrNo2");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeAl_OgrenciOneriNo",
                table: "ProjeAl",
                column: "OgrenciOneriNo");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeAl_ProjeNo",
                table: "ProjeAl",
                column: "ProjeNo");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeKoordinatoru_AkademisyenId",
                table: "ProjeKoordinatoru",
                column: "AkademisyenId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeOnerileri_DanismanId",
                table: "ProjeOnerileri",
                column: "DanismanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjeOnerileri_OturumNo",
                table: "ProjeOnerileri",
                column: "OturumNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Belgeler");

            migrationBuilder.DropTable(
                name: "Duyuru");

            migrationBuilder.DropTable(
                name: "EskiBasarisizAlinanProje");

            migrationBuilder.DropTable(
                name: "EskiKabulGorenProjeler");

            migrationBuilder.DropTable(
                name: "Istek");

            migrationBuilder.DropTable(
                name: "ProjeAl");

            migrationBuilder.DropTable(
                name: "Takvim");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProjeKoordinatoru");

            migrationBuilder.DropTable(
                name: "OgrenciProjeOnerisi");

            migrationBuilder.DropTable(
                name: "ProjeOnerileri");

            migrationBuilder.DropTable(
                name: "Ogrenci");

            migrationBuilder.DropTable(
                name: "AkademikPersonel");

            migrationBuilder.DropTable(
                name: "AlanOturum");
        }
    }
}
