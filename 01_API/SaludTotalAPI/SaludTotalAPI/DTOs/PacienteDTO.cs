namespace SaludTotalAPI.DTOs
{
    public class PacienteDTO
    {
        public long Id { get; set; }
        public string Dni { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string? ObraSocial { get; set; }
        public string Estado { get; set; } = null!;
    }

}

