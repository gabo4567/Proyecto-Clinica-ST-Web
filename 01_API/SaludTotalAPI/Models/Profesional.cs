using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("profesional")]
    public class Profesional
    {
        [Key]
        [Column("id_profesional")]
        public long IdProfesional { get; set; }

        [Required]
        [Column("dni")]
        [MaxLength(20)]
        public string Dni { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Column("direccion")]
        [MaxLength(255)]
        public string? Direccion { get; set; }

        [Required]
        [Column("genero")]
        [MaxLength(20)]
        public string Genero { get; set; }

        [Required]
        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("edad")]
        public int? Edad { get; set; }

        [Required]
        [Column("matricula")]
        [MaxLength(50)]
        public string Matricula { get; set; }

        [Column("id_especialidad")]
        public long IdEspecialidad { get; set; }

        [ForeignKey("IdEspecialidad")]
        public Especialidad? Especialidad { get; set; }

        [Column("id_estado")]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado? Estado { get; set; }
    }
}

