using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("estado")]
    public class Estado
    {
        [Key]
        [Column("id_estado")]
        public long IdEstado { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Column("descripcion")]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        // Relaciones con otras entidades (opcional si no se navega)
        public ICollection<Paciente>? Pacientes { get; set; }
        public ICollection<Profesional>? Profesionales { get; set; }
        public ICollection<Turno>? Turnos { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<Tutor>? Tutores { get; set; }
        public ICollection<Especialidad>? Especialidades { get; set; }
        public ICollection<HorarioDisponible>? Horarios { get; set; }
    }
}

