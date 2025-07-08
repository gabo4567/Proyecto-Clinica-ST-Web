namespace SaludTotalAPI.DTOs
{
    public class EspecialidadDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = null!;
    }

}
