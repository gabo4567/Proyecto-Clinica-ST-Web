namespace SaludTotalAPI.DTOs
{
    public class RegistroTurnoDTO
    {
        public long IdPaciente { get; set; }
        public long IdProfesional { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public string? Observaciones { get; set; }
    }
}
