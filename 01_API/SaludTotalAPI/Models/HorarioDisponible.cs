using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("Horario_Disponible")]
    public class HorarioDisponible
    {
        [Key]
        [Column("id_horario")]
        public int IdHorario { get; set; }

        [Required]
        [Column("id_profesional")]
        public long IdProfesional { get; set; }

        [ForeignKey("IdProfesional")]
        public Profesional? Profesional { get; set; }

        [Required]
        [Column("dia_semana")]
        public int DiaSemana { get; set; } // 1 = lunes ... 7 = domingo

        [Required]
        [Column("hora_inicio")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Column("hora_fin")]
        public TimeSpan HoraFin { get; set; }

        [Required]
        [Column("id_estado")]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado? Estado { get; set; }
    }
}
