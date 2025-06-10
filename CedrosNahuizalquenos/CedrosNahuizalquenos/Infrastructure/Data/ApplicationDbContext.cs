using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<HorasExtra> HorasExtras { get; set; }

    public virtual DbSet<Incidencia> Incidencias { get; set; }

    public virtual DbSet<UsuariosRrhh> UsuariosRrhhs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE6F0C34C9E10");

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PuestoFuncional).HasMaxLength(100);
            entity.Property(e => e.SalarioMensual).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<HorasExtra>(entity =>
        {
            entity.HasKey(e => e.HoraExtraId).HasName("PK__HorasExt__BEE2482F9B54CAB9");

            entity.Property(e => e.HoraExtraId).HasColumnName("HoraExtraID");
            entity.Property(e => e.CantidadHoras).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.Empleado).WithMany(p => p.HorasExtras)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorasExtras_Empleado");
        });

        modelBuilder.Entity<Incidencia>(entity =>
        {
            entity.HasKey(e => e.IncidenciaId).HasName("PK__Incidenc__E41133C63D43AE7D");

            entity.Property(e => e.IncidenciaId).HasColumnName("IncidenciaID");
            entity.Property(e => e.Comentario).HasMaxLength(250);
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.Empleado).WithMany(p => p.Incidencia)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Incidencias_Empleado");
        });

        modelBuilder.Entity<UsuariosRrhh>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7983EE55BDD");

            entity.ToTable("UsuariosRRHH");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19537C49EF").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });
        modelBuilder.Entity<ReporteResumenEmpleadoDto>().HasNoKey().ToView(null);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
