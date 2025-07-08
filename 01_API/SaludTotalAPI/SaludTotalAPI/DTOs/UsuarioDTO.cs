namespace SaludTotalAPI.DTOs
{
    public class RegistroUsuarioDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!; // "Paciente", "Secretaria", etc.
    }

    public class LoginDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }

    public class UsuarioAutenticadoDTO
    {
        public long Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}
