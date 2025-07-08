using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;
using SaludTotalAPI.Models;
using AutoMapper;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ Todos los endpoints requieren estar autenticado
    public class PacientesController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public PacientesController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ Todos los roles autenticados pueden consultar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDTO>>> GetPacientes()
        {
            var pacientes = await _context.Pacientes
                .Include(p => p.Estado)
                .ToListAsync();

            var dto = _mapper.Map<List<PacienteDTO>>(pacientes);
            return Ok(dto);
        }

        [HttpGet("{dni}")]
        public async Task<ActionResult<PacienteDTO>> GetPaciente(string dni)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(p => p.Dni == dni);

            if (paciente == null)
                return NotFound();

            return Ok(_mapper.Map<PacienteDTO>(paciente));
        }

        // ✅ Solo Admin y Secretaria pueden crear
        [HttpPost]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<ActionResult> CrearPaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { dni = paciente.Dni }, paciente);
        }

        // ✅ Solo Admin y Secretaria pueden editar
        [HttpPut("{dni}")]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<IActionResult> EditarPaciente(string dni, Paciente paciente)
        {
            if (dni != paciente.Dni)
                return BadRequest();

            var pacienteExistente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni);
            if (pacienteExistente == null)
                return NotFound();

            _mapper.Map(paciente, pacienteExistente); // Asume que usás AutoMapper

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ Solo Admin y Secretaria pueden eliminar (baja lógica)
        [HttpDelete("{dni}")]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<IActionResult> EliminarPaciente(string dni)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni);
            if (paciente == null)
                return NotFound();

            paciente.IdEstado = 2; // Inactivo
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

