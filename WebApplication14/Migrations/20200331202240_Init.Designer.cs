﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication14.Models;

namespace WebApplication14.Migrations
{
    [DbContext(typeof(AraProjeContext))]
    [Migration("20200331202240_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication14.Models.AkademikPersonel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Anabilimdali")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kisaltma")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KullaniciAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sifre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unvan")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AkademikPersonel");
                });

            modelBuilder.Entity("WebApplication14.Models.AlanOturum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AlanOturum");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApplication14.Models.Belgeler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ErisimYolu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Belgeler");
                });

            modelBuilder.Entity("WebApplication14.Models.Duyuru", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Baslik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Eklenti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icerik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KoordinatorNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Zaman")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KoordinatorNo");

                    b.ToTable("Duyuru");
                });

            modelBuilder.Entity("WebApplication14.Models.EskiBasarisizAlinanProje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Danismanid")
                        .HasColumnType("int");

                    b.Property<string>("Form2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ogrno1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ogrno2")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OturumNo")
                        .HasColumnType("int");

                    b.Property<string>("Statu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Danismanid");

                    b.HasIndex("Ogrno1");

                    b.HasIndex("Ogrno2");

                    b.HasIndex("OturumNo");

                    b.ToTable("EskiBasarisizAlinanProje");
                });

            modelBuilder.Entity("WebApplication14.Models.EskiKabulGorenProjeler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DanismanId")
                        .HasColumnType("int");

                    b.Property<string>("Form1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GrupSayisi")
                        .HasColumnType("int");

                    b.Property<string>("Isim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KisiSayisi")
                        .HasColumnType("int");

                    b.Property<int?>("OturumNo")
                        .HasColumnType("int");

                    b.Property<string>("Turu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DanismanId");

                    b.HasIndex("OturumNo");

                    b.ToTable("EskiKabulGorenProjeler");
                });

            modelBuilder.Entity("WebApplication14.Models.Istek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Form2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OgrNo1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OgrNo2")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ProjeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OgrNo1");

                    b.HasIndex("OgrNo2");

                    b.HasIndex("ProjeId");

                    b.ToTable("Istek");
                });

            modelBuilder.Entity("WebApplication14.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebApplication14.Models.Ogrenci", b =>
                {
                    b.Property<string>("OgrenciNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgrenciNo");

                    b.ToTable("Ogrenci");
                });

            modelBuilder.Entity("WebApplication14.Models.OgrenciProjeOnerisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Danismanid")
                        .HasColumnType("int");

                    b.Property<string>("Form2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ogrenci1No")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ogrenci2No")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OturumNo")
                        .HasColumnType("int");

                    b.Property<string>("Statu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Danismanid");

                    b.HasIndex("Ogrenci1No");

                    b.HasIndex("Ogrenci2No");

                    b.HasIndex("OturumNo");

                    b.ToTable("OgrenciProjeOnerisi");
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeAl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ararapor1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ararapor2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AsistanId")
                        .HasColumnType("int");

                    b.Property<string>("Butunlemekitap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Butunlemerapor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Finalkitap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Finalrapor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KabulDurumu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KurulAciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OgrNo1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OgrNo2")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OgrenciOneriNo")
                        .HasColumnType("int");

                    b.Property<string>("ProjeDurumu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjeNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AsistanId");

                    b.HasIndex("OgrNo1");

                    b.HasIndex("OgrNo2");

                    b.HasIndex("OgrenciOneriNo");

                    b.HasIndex("ProjeNo");

                    b.ToTable("ProjeAl");
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeKoordinatoru", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AkademisyenId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AkademisyenId");

                    b.ToTable("ProjeKoordinatoru");
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeOnerileri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DanismanId")
                        .HasColumnType("int");

                    b.Property<string>("Form1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GrupSayisi")
                        .HasColumnType("int");

                    b.Property<string>("Isim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kategori")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KisiSayisi")
                        .HasColumnType("int");

                    b.Property<int?>("OturumNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DanismanId");

                    b.HasIndex("OturumNo");

                    b.ToTable("ProjeOnerileri");
                });

            modelBuilder.Entity("WebApplication14.Models.Takvim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Arabütünleme")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ararapor1")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ararapor2")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Arasinav")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Bitirmebutunleme")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Bitirmesinav")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Bkitap")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Bütünlemerapor")
                        .HasColumnType("datetime2");

                    b.Property<string>("Donem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Finalrapor")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Form2")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Kitap")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ret")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Toplanti")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Takvim");
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetRoleClaims", b =>
                {
                    b.HasOne("WebApplication14.Models.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserClaims", b =>
                {
                    b.HasOne("WebApplication14.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserLogins", b =>
                {
                    b.HasOne("WebApplication14.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserRoles", b =>
                {
                    b.HasOne("WebApplication14.Models.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication14.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication14.Models.AspNetUserTokens", b =>
                {
                    b.HasOne("WebApplication14.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication14.Models.Duyuru", b =>
                {
                    b.HasOne("WebApplication14.Models.ProjeKoordinatoru", "Koordinator")
                        .WithMany("Duyuru")
                        .HasForeignKey("KoordinatorNo")
                        .HasConstraintName("FK__DUYURU__Koordina__5FB337D6")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.EskiBasarisizAlinanProje", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Danisman")
                        .WithMany("EskiBasarisizAlinanProje")
                        .HasForeignKey("Danismanid")
                        .HasConstraintName("FK__ESKI_BASA__DANIS__47DBAE45")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "Ogrno1Navigation")
                        .WithMany("EskiBasarisizAlinanProjeOgrno1Navigation")
                        .HasForeignKey("Ogrno1")
                        .HasConstraintName("FK__ESKI_BASA__OGRNO__45F365D3")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "Ogrno2Navigation")
                        .WithMany("EskiBasarisizAlinanProjeOgrno2Navigation")
                        .HasForeignKey("Ogrno2")
                        .HasConstraintName("FK__ESKI_BASA__OGRNO__46E78A0C");

                    b.HasOne("WebApplication14.Models.AlanOturum", "OturumNoNavigation")
                        .WithMany("EskiBasarisizAlinanProje")
                        .HasForeignKey("OturumNo")
                        .HasConstraintName("FK__ESKI_BASA__OTURU__48CFD27E")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.EskiKabulGorenProjeler", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Danisman")
                        .WithMany("EskiKabulGorenProjeler")
                        .HasForeignKey("DanismanId")
                        .HasConstraintName("FK__ESKI_KABU__DANIS__4222D4EF")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.AlanOturum", "OturumNoNavigation")
                        .WithMany("EskiKabulGorenProjeler")
                        .HasForeignKey("OturumNo")
                        .HasConstraintName("FK__ESKI_KABU__OTURU__4316F928")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.Istek", b =>
                {
                    b.HasOne("WebApplication14.Models.Ogrenci", "OgrNo1Navigation")
                        .WithMany("IstekOgrNo1Navigation")
                        .HasForeignKey("OgrNo1")
                        .HasConstraintName("FK__ISTEK__OgrNo1__2E1BDC42")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "OgrNo2Navigation")
                        .WithMany("IstekOgrNo2Navigation")
                        .HasForeignKey("OgrNo2")
                        .HasConstraintName("FK__ISTEK__OgrNo2__2F10007B");

                    b.HasOne("WebApplication14.Models.ProjeOnerileri", "Proje")
                        .WithMany("Istek")
                        .HasForeignKey("ProjeId")
                        .HasConstraintName("FK__ISTEK__ProjeID__2D27B809")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.OgrenciProjeOnerisi", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Danisman")
                        .WithMany("OgrenciProjeOnerisi")
                        .HasForeignKey("Danismanid")
                        .HasConstraintName("FK__OGRENCI_P__DANIS__31EC6D26")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "Ogrno1Navigation")
                        .WithMany("OgrenciProjeOnerisiOgrno1Navigation")
                        .HasForeignKey("Ogrenci1No")
                        .HasConstraintName("FK__OGRENCI_P__OGRNO__33D4B598")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "Ogrno2Navigation")
                        .WithMany("OgrenciProjeOnerisiOgrno2Navigation")
                        .HasForeignKey("Ogrenci2No")
                        .HasConstraintName("FK__OGRENCI_P__OGRNO__34C8D9D1");

                    b.HasOne("WebApplication14.Models.AlanOturum", "OturumNoNavigation")
                        .WithMany("OgrenciProjeOnerisi")
                        .HasForeignKey("OturumNo")
                        .HasConstraintName("FK__OGRENCI_P__OTURU__32E0915F")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeAl", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Asistan")
                        .WithMany("ProjeAl")
                        .HasForeignKey("AsistanId")
                        .HasConstraintName("FK__PROJE_AL__ASISTA__1AD3FDA4");

                    b.HasOne("WebApplication14.Models.Ogrenci", "OgrNo1Navigation")
                        .WithMany("ProjeAlOgrNo1Navigation")
                        .HasForeignKey("OgrNo1")
                        .HasConstraintName("FK__PROJE_AL__OGR_NO__29572725")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.Ogrenci", "OgrNo2Navigation")
                        .WithMany("ProjeAlOgrNo2Navigation")
                        .HasForeignKey("OgrNo2")
                        .HasConstraintName("FK__PROJE_AL__OGR_NO__2A4B4B5E");

                    b.HasOne("WebApplication14.Models.OgrenciProjeOnerisi", "OgrenciOneriNoNavigation")
                        .WithMany("ProjeAl")
                        .HasForeignKey("OgrenciOneriNo")
                        .HasConstraintName("FK__PROJE_AL__OGRENC__35BCFE0A");

                    b.HasOne("WebApplication14.Models.ProjeOnerileri", "ProjeNoNavigation")
                        .WithMany("ProjeAl")
                        .HasForeignKey("ProjeNo")
                        .HasConstraintName("FK__PROJE_AL__PROJE___286302EC")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeKoordinatoru", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Akademisyen")
                        .WithMany("ProjeKoordinatoru")
                        .HasForeignKey("AkademisyenId")
                        .HasConstraintName("FK__PROJE_KOO__Akade__5CD6CB2B")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication14.Models.ProjeOnerileri", b =>
                {
                    b.HasOne("WebApplication14.Models.AkademikPersonel", "Danisman")
                        .WithMany("ProjeOnerileri")
                        .HasForeignKey("DanismanId")
                        .HasConstraintName("FK__PROJE_ONE__DANIS__145C0A3F")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication14.Models.AlanOturum", "OturumNoNavigation")
                        .WithMany("ProjeOnerileri")
                        .HasForeignKey("OturumNo")
                        .HasConstraintName("FK__PROJE_ONE__OTURU__15502E78")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
