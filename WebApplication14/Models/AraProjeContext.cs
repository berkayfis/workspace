using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication14.Models
{
    public partial class AraProjeContext : DbContext
    {
        public AraProjeContext()
        {
        }

        public AraProjeContext(DbContextOptions<AraProjeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AkademikPersonel> AkademikPersonel { get; set; }
        public virtual DbSet<AlanOturum> AlanOturum { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Belgeler> Belgeler { get; set; }
        public virtual DbSet<Duyuru> Duyuru { get; set; }
        public virtual DbSet<EskiBasarisizAlinanProje> EskiBasarisizAlinanProje { get; set; }
        public virtual DbSet<EskiKabulGorenProjeler> EskiKabulGorenProjeler { get; set; }
        public virtual DbSet<Istek> Istek { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<OgrenciProjeOnerisi> OgrenciProjeOnerisi { get; set; }
        public virtual DbSet<ProjeAl> ProjeAl { get; set; }
        public virtual DbSet<ProjeKoordinatoru> ProjeKoordinatoru { get; set; }
        public virtual DbSet<ProjeOnerileri> ProjeOnerileri { get; set; }
        public virtual DbSet<Takvim> Takvim { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer("Server = DESKTOP-PLI0TQD; Database = AraProje; Trusted_Connection=True;");

        //    }
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            

            modelBuilder.Entity<Duyuru>(entity =>
            {
                entity.HasOne(d => d.Koordinator)
                    .WithMany(p => p.Duyuru)
                    .HasForeignKey(d => d.KoordinatorNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DUYURU__Koordina__5FB337D6");
            });

            modelBuilder.Entity<EskiBasarisizAlinanProje>(entity =>
            {
                entity.HasOne(d => d.Danisman)
                    .WithMany(p => p.EskiBasarisizAlinanProje)
                    .HasForeignKey(d => d.Danismanid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ESKI_BASA__DANIS__47DBAE45");

                entity.HasOne(d => d.Ogrno1Navigation)
                    .WithMany(p => p.EskiBasarisizAlinanProjeOgrno1Navigation)
                    .HasForeignKey(d => d.Ogrno1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ESKI_BASA__OGRNO__45F365D3");

                entity.HasOne(d => d.Ogrno2Navigation)
                    .WithMany(p => p.EskiBasarisizAlinanProjeOgrno2Navigation)
                    .HasForeignKey(d => d.Ogrno2)
                    .HasConstraintName("FK__ESKI_BASA__OGRNO__46E78A0C");

                entity.HasOne(d => d.OturumNoNavigation)
                    .WithMany(p => p.EskiBasarisizAlinanProje)
                    .HasForeignKey(d => d.OturumNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ESKI_BASA__OTURU__48CFD27E");
            });

            modelBuilder.Entity<EskiKabulGorenProjeler>(entity =>
            {
                entity.HasOne(d => d.Danisman)
                    .WithMany(p => p.EskiKabulGorenProjeler)
                    .HasForeignKey(d => d.DanismanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ESKI_KABU__DANIS__4222D4EF");

                entity.HasOne(d => d.OturumNoNavigation)
                    .WithMany(p => p.EskiKabulGorenProjeler)
                    .HasForeignKey(d => d.OturumNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ESKI_KABU__OTURU__4316F928");
            });

            modelBuilder.Entity<Istek>(entity =>
            {
                entity.HasOne(d => d.OgrNo1Navigation)
                    .WithMany(p => p.IstekOgrNo1Navigation)
                    .HasForeignKey(d => d.OgrNo1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ISTEK__OgrNo1__2E1BDC42");

                entity.HasOne(d => d.OgrNo2Navigation)
                    .WithMany(p => p.IstekOgrNo2Navigation)
                    .HasForeignKey(d => d.OgrNo2)
                    .HasConstraintName("FK__ISTEK__OgrNo2__2F10007B");

                entity.HasOne(d => d.Proje)
                    .WithMany(p => p.Istek)
                    .HasForeignKey(d => d.ProjeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ISTEK__ProjeID__2D27B809");
            });

            modelBuilder.Entity<OgrenciProjeOnerisi>(entity =>
            {
                entity.HasOne(d => d.Danisman)
                    .WithMany(p => p.OgrenciProjeOnerisi)
                    .HasForeignKey(d => d.Danismanid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__OGRENCI_P__DANIS__31EC6D26");

                entity.HasOne(d => d.Ogrno1Navigation)
                    .WithMany(p => p.OgrenciProjeOnerisiOgrno1Navigation)
                    .HasForeignKey(d => d.Ogrno1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__OGRENCI_P__OGRNO__33D4B598");

                entity.HasOne(d => d.Ogrno2Navigation)
                    .WithMany(p => p.OgrenciProjeOnerisiOgrno2Navigation)
                    .HasForeignKey(d => d.Ogrno2)
                    .HasConstraintName("FK__OGRENCI_P__OGRNO__34C8D9D1");

                entity.HasOne(d => d.OturumNoNavigation)
                    .WithMany(p => p.OgrenciProjeOnerisi)
                    .HasForeignKey(d => d.OturumNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__OGRENCI_P__OTURU__32E0915F");
            });

            modelBuilder.Entity<ProjeAl>(entity =>
            {
                entity.HasOne(d => d.Asistan)
                    .WithMany(p => p.ProjeAl)
                    .HasForeignKey(d => d.AsistanId)
                    .HasConstraintName("FK__PROJE_AL__ASISTA__1AD3FDA4");

                entity.HasOne(d => d.OgrNo1Navigation)
                    .WithMany(p => p.ProjeAlOgrNo1Navigation)
                    .HasForeignKey(d => d.OgrNo1)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PROJE_AL__OGR_NO__29572725");

                entity.HasOne(d => d.OgrNo2Navigation)
                    .WithMany(p => p.ProjeAlOgrNo2Navigation)
                    .HasForeignKey(d => d.OgrNo2)
                    .HasConstraintName("FK__PROJE_AL__OGR_NO__2A4B4B5E");

                entity.HasOne(d => d.OgrenciOneriNoNavigation)
                    .WithMany(p => p.ProjeAl)
                    .HasForeignKey(d => d.OgrenciOneriNo)
                    .HasConstraintName("FK__PROJE_AL__OGRENC__35BCFE0A");

                entity.HasOne(d => d.ProjeNoNavigation)
                    .WithMany(p => p.ProjeAl)
                    .HasForeignKey(d => d.ProjeNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PROJE_AL__PROJE___286302EC");
            });

            modelBuilder.Entity<ProjeKoordinatoru>(entity =>
            {
                entity.HasOne(d => d.Akademisyen)
                    .WithMany(p => p.ProjeKoordinatoru)
                    .HasForeignKey(d => d.AkademisyenId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PROJE_KOO__Akade__5CD6CB2B");
            });

            modelBuilder.Entity<ProjeOnerileri>(entity =>
            {
                entity.HasOne(d => d.Danisman)
                    .WithMany(p => p.ProjeOnerileri)
                    .HasForeignKey(d => d.DanismanId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PROJE_ONE__DANIS__145C0A3F");

                entity.HasOne(d => d.OturumNoNavigation)
                    .WithMany(p => p.ProjeOnerileri)
                    .HasForeignKey(d => d.OturumNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PROJE_ONE__OTURU__15502E78");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
