using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("turno")]
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public long IdTurno { get; set; }

        [Required]
        [Column("comprobante")]
        [MaxLength(50)]
        public string Comprobante { get; set; }

        [Required]
        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; }

        [Required]
        [Column("id_profesional")]
        public long IdProfesional { get; set; }

        [ForeignKey("IdProfesional")]
        public Profesional? Profesional { get; set; }

        [Column("id_tutor")]
        public long? IdTutor { get; set; }

        [ForeignKey("IdTutor")]
        public Tutor? Tutor { get; set; }

        [Required]
        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }

        [Required]
        [Column("duracion")]
        public int Duracion { get; set; }

        [Required]
        [Column("id_estado")]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado? Estado { get; set; }

        [Column("observaciones")]
        [MaxLength(500)]
        public string? Observaciones { get; set; }

        [Required]
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}

