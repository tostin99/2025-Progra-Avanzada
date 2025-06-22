using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyLibrary.Models;

public partial class MyLibraryContext : DbContext
{
    public MyLibraryContext()
    {
    }

    public MyLibraryContext(DbContextOptions<MyLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ejemplare> Ejemplares { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:srv-netcore.database.windows.net,1433;Initial Catalog=adventureworkslt;User ID=PrograAvanzada202502User04;Password=**MyVeryStr0ngP@ssw0rd1!**;Encrypt=True;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PrograAvanzada202502User04");

        modelBuilder.Entity<Ejemplare>(entity =>
        {
            entity.HasKey(e => e.EjemplarId).HasName("PK__Ejemplar__C7803E49EAF4CC76");

            entity.Property(e => e.Estado).HasMaxLength(50);

            entity.HasOne(d => d.Libro).WithMany(p => p.Ejemplares)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Ejemplare__Libro__50FB042B");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F7682D8010C6EC9");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libros__35A1ECED958AE7E5");

            entity.Property(e => e.Autor).HasMaxLength(100);
            entity.Property(e => e.Titulo).HasMaxLength(200);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.PrestamoId).HasName("PK__Prestamo__AA58A0A02F124610");

            entity.HasOne(d => d.Ejemplar).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.EjemplarId)
                .HasConstraintName("FK__Prestamos__Ejemp__55BFB948");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__Prestamos__Estud__56B3DD81");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
