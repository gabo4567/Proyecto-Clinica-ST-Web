using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs.Usuarios;
using SaludTotalAPI.Models;
using System.Security.Cryptography;

namespace SaludTotalAPI.Services
{
    public class AuthService
    {
        private readonly ClinicaContext _context;
        private readonly IConfiguration _config;

        public AuthService(ClinicaContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> UsuarioExiste(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario> RegistrarUsuario(string nombreUsuario, string contrasena)
        {
            var hash = HashPassword(contrasena);

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Contrasena = hash,
                IdEstado = 1 // Activo
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario && u.IdEstado == 1);

            if (usuario == null || !VerificarPassword(contrasena, usuario.Contrasena))
                return null;

            return usuario;
        }

        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Hasheo seguro
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerificarPassword(string password, string hashed)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashed;
        }
    }
}

