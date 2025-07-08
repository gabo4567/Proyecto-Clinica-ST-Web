using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;
using SaludTotalAPI.Models;
using SaludTotalAPI.Services;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public AuthController(ClinicaContext context, IMapper mapper, JwtService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // POST: api/Auth/registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroUsuarioDTO dto)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario);
            if (existe)
                return BadRequest("El nombre de usuario ya está en uso.");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);
            usuario.IdEstado = 1; // Activo por defecto

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado correctamente." });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena))
                return Unauthorized("Credenciales inválidas.");

            if (usuario.IdEstado == 2) // Inactivo
                return Unauthorized("El usuario está inactivo.");

            var token = _jwtService.GenerateToken(usuario);

            var respuesta = new UsuarioAutenticadoDTO
            {
                Id = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Rol = usuario.NombreUsuario.ToLower().Contains("admin") ? "Admin" : "Usuario", // o leer campo Rol si lo usás
                Token = token
            };

            return Ok(respuesta);
        }
    }
}

