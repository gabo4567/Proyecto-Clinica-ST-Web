using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public long IdUsuario { get; set; }

        [Required]
        [Column("nombre_usuario")]
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [Column("contrasena")]
        [MaxLength(100)]
        public string Contrasena { get; set; }

        [Required]
        [Column("rol")]
        [MaxLength(50)]
        public string Rol { get; set; }


        [Required]
        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("id_estado")]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estado? Estado { get; set; }
    }
}
