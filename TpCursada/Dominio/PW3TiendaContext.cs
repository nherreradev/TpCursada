using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TpCursada.Dominio
{
    public partial class PW3TiendaContext : DbContext
    {
        public PW3TiendaContext()
        {
        }

        public PW3TiendaContext(DbContextOptions<PW3TiendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Historical> Historicals { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductSalesHistory> ProductSalesHistories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=EFCoreContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Historical>(entity =>
            {
                entity.ToTable("historical");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCoproducto).HasColumnName("idCoproducto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Puntaje).HasColumnName("puntaje");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<ProductSalesHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("product sales-history");

                entity.Property(e => e.IdCoproducto).HasColumnName("id_coproducto");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.HasOne(d => d.IdCoproductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCoproducto)
                    .HasConstraintName("FK__HISTORIAL__id_co__38996AB5");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__HISTORIAL__id_pr__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
