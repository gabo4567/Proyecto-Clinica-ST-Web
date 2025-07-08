namespace SaludTotalAPI.DTOs
{
    public class EstadoDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }

}
