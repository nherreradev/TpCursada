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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CHONII;Database=PW3Tienda;User Id=sa;Password=1234;Trusted_Connection=True;");
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

                entity.HasOne(d => d.IdCoproductoNavigation)
                    .WithMany(p => p.HistoricalIdCoproductoNavigations)
                    .HasForeignKey(d => d.IdCoproducto)
                    .HasConstraintName("FK__historica__idCop__4CA06362");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.HistoricalIdProductoNavigations)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__historica__idPro__4BAC3F29");
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
                    .HasConstraintName("FK__product s__id_co__4F7CD00D");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__product s__id_pr__4E88ABD4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
