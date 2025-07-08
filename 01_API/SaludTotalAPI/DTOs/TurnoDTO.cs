namespace SaludTotalAPI.DTOs
{
    public class TurnoDTO
    {
        public long Id { get; set; }
        public string Comprobante { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; } = string.Empty;

        public PacienteMiniDTO Paciente { get; set; } = null!;
        public ProfesionalMiniDTO Profesional { get; set; } = null!;
        public string? Observaciones { get; set; }
    }
}

