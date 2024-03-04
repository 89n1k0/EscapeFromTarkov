using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EscapeFromTarkov
{
    public partial class EscapeFromTarkovContext : DbContext
    {
        public EscapeFromTarkovContext()
        {
        }

        public EscapeFromTarkovContext(DbContextOptions<EscapeFromTarkovContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Босс> Боссs { get; set; } = null!;
        public virtual DbSet<Выходы> Выходыs { get; set; } = null!;
        public virtual DbSet<Карта> Картаs { get; set; } = null!;
        public virtual DbSet<Оружие> Оружиеs { get; set; } = null!;
        public virtual DbSet<Персонажи> Персонажиs { get; set; } = null!;
        public virtual DbSet<Пользователь> Пользовательs { get; set; } = null!;
        public virtual DbSet<Роли> Ролиs { get; set; } = null!;
        public virtual DbSet<Сборка> Сборкаs { get; set; } = null!;
        public virtual DbSet<Товары> Товарыs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4GCEH3I;Database=EscapeFromTarkov;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Босс>(entity =>
            {
                entity.ToTable("Босс");

                entity.Property(e => e.БоссId).HasColumnName("БоссID");

                entity.Property(e => e.Изображение).HasColumnType("image");

                entity.Property(e => e.Наименование).HasMaxLength(100);
            });

            modelBuilder.Entity<Выходы>(entity =>
            {
                entity.ToTable("Выходы");

                entity.Property(e => e.ВыходыId).HasColumnName("ВыходыID");

                entity.Property(e => e.КартаId).HasColumnName("КартаID");

                entity.Property(e => e.Наименование).HasMaxLength(100);

                entity.Property(e => e.ПерсонажиId).HasColumnName("ПерсонажиID");

                entity.HasOne(d => d.Карта)
                    .WithMany(p => p.Выходыs)
                    .HasForeignKey(d => d.КартаId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Выходы__КартаID__4316F928");

                entity.HasOne(d => d.Персонажи)
                    .WithMany(p => p.Выходыs)
                    .HasForeignKey(d => d.ПерсонажиId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Выходы__Персонаж__440B1D61");
            });

            modelBuilder.Entity<Карта>(entity =>
            {
                entity.ToTable("Карта");

                entity.Property(e => e.КартаId).HasColumnName("КартаID");

                entity.Property(e => e.Изображение).HasColumnType("image");

                entity.Property(e => e.Наименование).HasMaxLength(100);
            });

            modelBuilder.Entity<Оружие>(entity =>
            {
                entity.ToTable("Оружие");

                entity.Property(e => e.ОружиеId).HasColumnName("ОружиеID");

                entity.Property(e => e.Наименование).HasMaxLength(100);
            });

            modelBuilder.Entity<Персонажи>(entity =>
            {
                entity.ToTable("Персонажи");

                entity.Property(e => e.ПерсонажиId).HasColumnName("ПерсонажиID");

                entity.Property(e => e.Изображение).HasColumnType("image");

                entity.Property(e => e.Наименование).HasMaxLength(100);
            });

            modelBuilder.Entity<Пользователь>(entity =>
            {
                entity.ToTable("Пользователь");

                entity.Property(e => e.ПользовательId).HasColumnName("ПользовательID");

                entity.Property(e => e.Доказательство).HasColumnType("image");

                entity.Property(e => e.КартаId).HasColumnName("КартаID");

                entity.Property(e => e.Логин).HasMaxLength(100);

                entity.Property(e => e.Пароль).HasMaxLength(100);

                entity.Property(e => e.РолиId).HasColumnName("РолиID");

                entity.Property(e => e.УбийстваЧвк).HasColumnName("УбийстваЧВК");

                entity.HasOne(d => d.Карта)
                    .WithMany(p => p.Пользовательs)
                    .HasForeignKey(d => d.КартаId)
                    .HasConstraintName("FK__Пользоват__Карта__3D5E1FD2");

                entity.HasOne(d => d.Роли)
                    .WithMany(p => p.Пользовательs)
                    .HasForeignKey(d => d.РолиId)
                    .HasConstraintName("FK__Пользоват__РолиI__3E52440B");
            });

            modelBuilder.Entity<Роли>(entity =>
            {
                entity.ToTable("Роли");

                entity.Property(e => e.РолиId).HasColumnName("РолиID");

                entity.Property(e => e.Наименование).HasMaxLength(100);
            });

            modelBuilder.Entity<Сборка>(entity =>
            {
                entity.ToTable("Сборка");

                entity.Property(e => e.СборкаId).HasColumnName("СборкаID");

                entity.Property(e => e.Изображение).HasColumnType("image");

                entity.Property(e => e.Наименование).HasMaxLength(100);

                entity.Property(e => e.ОружиеId).HasColumnName("ОружиеID");

                entity.HasOne(d => d.Оружие)
                    .WithMany(p => p.Сборкаs)
                    .HasForeignKey(d => d.ОружиеId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Сборка__ОружиеID__48CFD27E");
            });

            modelBuilder.Entity<Товары>(entity =>
            {
                entity.HasKey(e => e.Udid)
                    .HasName("PK__Товары__5E657BBF24D4E6A8");

                entity.ToTable("Товары");

                entity.Property(e => e.Udid)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Единица).HasMaxLength(3);

                entity.Property(e => e.Название).HasMaxLength(100);

                entity.Property(e => e.Торговец).HasMaxLength(100);

                entity.Property(e => e.Цена).HasColumnType("money");

                entity.Property(e => e.Цена24часа).HasColumnType("money");

                entity.Property(e => e.Цена7дней).HasColumnType("money");

                entity.Property(e => e.ЦенаОбратногоВыкупа).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
