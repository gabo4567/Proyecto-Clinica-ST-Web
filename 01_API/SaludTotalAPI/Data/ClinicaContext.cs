using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Models;

namespace SaludTotalAPI.Data
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nombre de tabla singular evita pluralización automática
            modelBuilder.Entity<Paciente>().ToTable("paciente");
            modelBuilder.Entity<Profesional>().ToTable("profesional");
            modelBuilder.Entity<Tutor>().ToTable("tutor");
            modelBuilder.Entity<Estado>().ToTable("estado");
            modelBuilder.Entity<Especialidad>().ToTable("especialidad");
            modelBuilder.Entity<Turno>().ToTable("turno");
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<HorarioDisponible>().ToTable("Horario_Disponible");
        }
    }
}



