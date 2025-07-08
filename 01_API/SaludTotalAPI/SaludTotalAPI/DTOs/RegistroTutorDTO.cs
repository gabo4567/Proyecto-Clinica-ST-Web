namespace SaludTotalAPI.DTOs
{
    public class RegistroTutorDTO
    {
        public string Dni { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string? ObraSocial { get; set; }
        public string? Parentesco { get; set; }
        public long IdPaciente { get; set; }
    }

}
