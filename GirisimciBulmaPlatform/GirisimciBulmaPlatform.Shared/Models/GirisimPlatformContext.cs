using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GirisimciBulmaPlatform.Shared.Models
{
    public partial class GirisimPlatformContext : DbContext
    {
        public GirisimPlatformContext()
        {
        }

        public GirisimPlatformContext(DbContextOptions<GirisimPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fikir> Fikirs { get; set; } = null!;
        public virtual DbSet<Fotoraf> Fotorafs { get; set; } = null!;
        public virtual DbSet<Katagori> Katagoris { get; set; } = null!;
        public virtual DbSet<Kullanici> Kullanicis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MuratBayir;Database=GirisimPlatform;User Id=sa;Password=Murat2000;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fikir>(entity =>
            {
                entity.ToTable("Fikir");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FikirAciklama)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.FikirBaslik)
                    .HasMaxLength(120)
                    .IsFixedLength();

                entity.HasOne(d => d.Katagori)
                    .WithMany(p => p.Fikirs)
                    .HasForeignKey(d => d.KatagoriId)
                    .HasConstraintName("FK_Fikir_Katagori");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Fikirs)
                    .HasForeignKey(d => d.KullaniciId)
                    .HasConstraintName("FK_Fikir_Kullanici");
            });

            modelBuilder.Entity<Fotoraf>(entity =>
            {
                entity.ToTable("Fotoraf");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BosAlan).HasMaxLength(10);

                entity.Property(e => e.FotoAciklama).HasMaxLength(100);

                entity.HasOne(d => d.Fikir)
                    .WithMany(p => p.Fotorafs)
                    .HasForeignKey(d => d.FikirId)
                    .HasConstraintName("FK_Fotoraf_Fikir");
            });

            modelBuilder.Entity<Katagori>(entity =>
            {
                entity.ToTable("Katagori");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KatagoriAdi)
                    .HasMaxLength(60)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.ToTable("Kullanici");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdSoyad)
                    .HasMaxLength(80)
                    .IsFixedLength();

                entity.Property(e => e.DogumTarihi).HasColumnType("datetime");

                entity.Property(e => e.KullaniciAdi).HasMaxLength(60);

                entity.Property(e => e.KullaniciSifre)
                    .HasMaxLength(60)
                    .IsFixedLength();

                entity.Property(e => e.MailAdres)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Telefon)
                    .HasMaxLength(40)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
