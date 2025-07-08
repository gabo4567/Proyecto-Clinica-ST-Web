using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("especialidad")]
    public class Especialidad
    {
        [Key]
        [Column("id_especialidad")]
        public long IdEspecialidad { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("descripcion")]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Column("id_estado")]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado? Estado { get; set; }
    }
}
