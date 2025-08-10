using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class VetContext : DbContext
{
    public VetContext()
    {
    }

    public VetContext(DbContextOptions<VetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animales> Animales { get; set; }

    public virtual DbSet<Citas> Citas { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Doctores> Doctores { get; set; }

    public virtual DbSet<HistorialCitas> HistorialCitas { get; set; }

    public virtual DbSet<HorariosDoctor> HorariosDoctor { get; set; }

    public virtual DbSet<TiposAnimales> TiposAnimales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PrograAvanzada202502User04");

        modelBuilder.Entity<Animales>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PK__Animales__A21A7307E0F64CBA");

            entity.Property(e => e.Sexo).IsFixedLength();

            entity.HasOne(d => d.Cliente).WithMany(p => p.Animales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Animales_Cliente");

            entity.HasOne(d => d.TipoAnimal).WithMany(p => p.Animales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Animales_Tipo");
        });

        modelBuilder.Entity<Citas>(entity =>
        {
            entity.HasKey(e => e.CitaId).HasName("PK__Citas__F0E2D9D26C19D48E");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Animal).WithMany(p => p.Citas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cita_Animal");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Citas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cita_Doctor");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD087E49BE914");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Doctores>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctores__2DC00EBFB94CC6B8");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<HistorialCitas>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__9752068F88B3C377");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Cita).WithMany(p => p.HistorialCitas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Cita");
        });

        modelBuilder.Entity<HorariosDoctor>(entity =>
        {
            entity.HasKey(e => e.HorarioId).HasName("PK__Horarios__BB881B7E76F6DD72");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HorariosDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horario_Doctor");
        });

        modelBuilder.Entity<TiposAnimales>(entity =>
        {
            entity.HasKey(e => e.TipoAnimalId).HasName("PK__TiposAni__70437757387F0389");
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
