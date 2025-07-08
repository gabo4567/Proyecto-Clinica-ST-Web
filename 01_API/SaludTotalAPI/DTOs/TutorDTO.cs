namespace SaludTotalAPI.DTOs
{
    public class TutorDTO
    {
        public long Id { get; set; }
        public string Dni { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string? ObraSocial { get; set; }
        public string? Parentesco { get; set; }

        public PacienteMiniDTO Paciente { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }


}
