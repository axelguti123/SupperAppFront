using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model.Model;

public partial class DbObraContext : DbContext
{
    public DbObraContext()
    {
    }

    public DbObraContext(DbContextOptions<DbObraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDetallePartidum> TblDetallePartida { get; set; }

    public virtual DbSet<TblEspecialidad> TblEspecialidads { get; set; }

    public virtual DbSet<TblPartidum> TblPartida { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; Database=dbObra; Trusted_Connection=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblDetallePartidum>(entity =>
        {
            entity.HasKey(e => e.IddetallePartida).HasName("PK__tblDetal__1D5D116264EF2DAD");

            entity.ToTable("tblDetallePartida");

            entity.Property(e => e.IddetallePartida).HasColumnName("IDDetallePartida");
            entity.Property(e => e.Adicional).HasColumnName("adicional");
            entity.Property(e => e.CantEjecutada)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("cantEjecutada");
            entity.Property(e => e.CantFaltante)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("cantFaltante");
            entity.Property(e => e.CantProyectada)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("cantProyectada");
            entity.Property(e => e.CodPartida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codPartida");
            entity.Property(e => e.Porcentaje)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("porcentaje");

            entity.HasOne(d => d.CodPartidaNavigation).WithMany(p => p.TblDetallePartida)
                .HasForeignKey(d => d.CodPartida)
                .HasConstraintName("FK__tblDetall__codPa__29221CFB");
        });

        modelBuilder.Entity<TblEspecialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__tblEspec__E8AB1600D02440D0");

            entity.ToTable("tblEspecialidad");

            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NombreEspecialidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEspecialidad");
        });

        modelBuilder.Entity<TblPartidum>(entity =>
        {
            entity.HasKey(e => e.CodPartida).HasName("PK__tblParti__FF1A7069A4D7A95F");

            entity.ToTable("tblPartida");

            entity.Property(e => e.CodPartida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codPartida");
            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.IdPadre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Partida)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("partida");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("total");
            entity.Property(e => e.Und)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.TblPartida)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__tblPartid__idEsp__25518C17");

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.InverseIdPadreNavigation)
                .HasForeignKey(d => d.IdPadre)
                .HasConstraintName("FK__tblPartid__IdPad__2645B050");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__tblUsuar__645723A6114AEAA0");

            entity.ToTable("tblUsuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.FechaCreacion).HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreDeUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_de_usuario");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__tblUsuari__idEsp__22751F6C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
