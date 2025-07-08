namespace SaludTotalAPI.DTOs
{
    public class ProfesionalDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
        public long IdEspecialidad { get; set; }
        public string Estado { get; set; } = null!;
    }

}
