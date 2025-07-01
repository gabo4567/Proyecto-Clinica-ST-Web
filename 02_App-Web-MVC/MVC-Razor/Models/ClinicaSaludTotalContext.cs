using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace V2Razor.Models;

public partial class ClinicaSaludTotalContext : DbContext
{
    public ClinicaSaludTotalContext()
    {
    }

    public ClinicaSaludTotalContext(DbContextOptions<ClinicaSaludTotalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HorarioDisponible> HorarioDisponibles { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Profesional> Profesionals { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CLVSJ8F;Database=clinica_salud_total;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__especial__C1D1376397AA01E6");

            entity.ToTable("especialidad");

            entity.HasIndex(e => e.Nombre, "UQ__especial__72AFBCC6AF244262").IsUnique();

            entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Especialidads)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__especiali__id_es__3A81B327");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__estado__86989FB2B3B4A584");

            entity.ToTable("estado");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<HorarioDisponible>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario___C5836D69CCEF79F8");

            entity.ToTable("Horario_Disponible");

            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.DiaSemana).HasColumnName("dia_semana");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.HorarioDisponibles)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Horario_D__id_es__5CD6CB2B");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.HorarioDisponibles)
                .HasForeignKey(d => d.IdProfesional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Horario_D__id_pr__5BE2A6F2");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__paciente__2C2C72BBB3A01948");

            entity.ToTable("paciente");

            entity.HasIndex(e => e.Email, "UQ__paciente__AB6E6164C25B6840").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__paciente__D87608A755D0AB70").IsUnique();

            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.ObraSocial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obra_social");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estado");
        });

        modelBuilder.Entity<Profesional>(entity =>
        {
            entity.HasKey(e => e.IdProfesional).HasName("PK__profesio__F6DC93F2AE19FB8D");

            entity.ToTable("profesional");

            entity.HasIndex(e => e.Matricula, "UQ__profesio__30962D1561DD8213").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__profesio__AB6E6164B60498EB").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__profesio__D87608A7C416C4A6").IsUnique();

            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Matricula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Profesionals)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_especialidad");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Profesionals)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estado_prof");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__turno__C68E7397E9CA9E7C");

            entity.ToTable("turno");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.Comprobante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("comprobante");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaHora).HasColumnName("fecha_hora");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");
            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.Property(e => e.Observaciones)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_turno_estado");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_turno_paciente");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdProfesional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_turno_profesional");

            entity.HasOne(d => d.IdTutorNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdTutor)
                .HasConstraintName("fk_turno_tutor");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.IdTutor).HasName("PK__tutor__C176593DDBEB5A86");

            entity.ToTable("tutor");

            entity.HasIndex(e => e.Email, "UQ__tutor__AB6E6164A6F3A49A").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__tutor__D87608A73134866E").IsUnique();

            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.ObraSocial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obra_social");
            entity.Property(e => e.Parentesco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("parentesco");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Tutors)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tutor_estado");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Tutors)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tutor_paciente");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__4E3E04AD57303ADA");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.NombreUsuario, "UQ__usuario__D4D22D745848B7B3").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
