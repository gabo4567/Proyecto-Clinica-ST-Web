using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperacionController : ControllerBase
    {
        private static Dictionary<string, string> tokensTemporales = new(); // Email -> Token

        private readonly ClinicaContext _context;

        public RecuperacionController(ClinicaContext context)
        {
            _context = context;
        }

        // Paso 1: Solicita recuperación
        [HttpPost("solicitar")]
        public async Task<IActionResult> SolicitarRecuperacion([FromBody] RecuperarContrasenaDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null)
                return NotFound("Email no registrado");

            // Generar token simple
            string token = Guid.NewGuid().ToString("N");

            // Guardar token temporalmente (en memoria)
            tokensTemporales[dto.Email] = token;

            // Simular envío por consola (en vez de email real)
            Console.WriteLine($"🔐 Token de recuperación para {dto.Email}: {token}");

            return Ok("Token enviado (simulado por consola)");
        }

        // Paso 2: Confirmar nueva contraseña
        [HttpPost("confirmar")]
        public async Task<IActionResult> ConfirmarNuevaContrasena([FromBody] ConfirmarNuevaContrasenaDTO dto)
        {
            // Buscar el email que tenga ese token
            var email = tokensTemporales.FirstOrDefault(t => t.Value == dto.Token).Key;

            if (string.IsNullOrEmpty(email))
                return BadRequest("Token inválido");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            // Hashear nueva contraseña
            string hashed = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(dto.NuevaContrasena)));
            usuario.Contrasena = hashed;

            await _context.SaveChangesAsync();

            // Eliminar token una vez usado
            tokensTemporales.Remove(email);

            return Ok("Contraseña actualizada correctamente");
        }
    }
}

