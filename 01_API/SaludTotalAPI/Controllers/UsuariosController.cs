using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;
using SaludTotalAPI.Models;
using AutoMapper;
using SaludTotalAPI.Services;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public UsuariosController(ClinicaContext context, JwtService jwtService, IMapper mapper)
        {
            _context = context;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        // POST: api/Usuarios/registro
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDTO>> RegistrarUsuario(RegistroUsuarioDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario))
                return BadRequest("Nombre de usuario ya registrado.");

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.IdEstado = 1;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return CreatedAtAction(nameof(ObtenerUsuario), new { id = usuario.IdUsuario }, usuarioDTO);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> ObtenerUsuario(long id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
                return NotFound();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioAutenticadoDTO>> Login(LoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario && u.Contrasena == dto.Contrasena);

            if (usuario == null || usuario.IdEstado != 1)
                return Unauthorized("Usuario o contraseña incorrectos.");

            var token = _jwtService.GenerarToken(usuario);
            var dtoRespuesta = new UsuarioAutenticadoDTO
            {
                Id = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email ?? "",
                Rol = usuario.Rol ?? "Paciente",
                Token = token
            };

            return Ok(dtoRespuesta);
        }
    }
}

