namespace SaludTotalAPI.DTOs
{
    public class HorarioDisponibleDTO
    {
        public int Id { get; set; }
        public string DiaSemana { get; set; } = null!;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Estado { get; set; } = null!;
    }

}
