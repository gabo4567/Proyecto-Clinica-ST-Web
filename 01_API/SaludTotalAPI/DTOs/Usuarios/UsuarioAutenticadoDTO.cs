namespace SaludTotalAPI.DTOs.Usuarios
{
    public class UsuarioAutenticadoDTO
    {
        public long Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}

