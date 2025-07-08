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
    [Authorize]
    public class TurnosController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public TurnosController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/turnos
        [HttpGet]
        [Authorize(Roles = "Admin,Secretaria, Admin")]
        public async Task<ActionResult<IEnumerable<TurnoDTO>>> GetTurnos()
        {
            var turnos = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional).ThenInclude(p => p.Especialidad)
                .Include(t => t.Estado)
                .ToListAsync();

            return Ok(_mapper.Map<List<TurnoDTO>>(turnos));
        }

        // GET: api/turnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TurnoDTO>> GetTurno(long id)
        {
            var turno = await _context.Turnos
                .Include(t => t.Paciente)
                .Include(t => t.Profesional).ThenInclude(p => p.Especialidad)
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(t => t.IdTurno == id);

            if (turno == null)
                return NotFound();

            return Ok(_mapper.Map<TurnoDTO>(turno));
        }

        // POST: api/turnos
        [HttpPost]
        [Authorize(Roles = "Admin,Secretaria,Paciente")]
        public async Task<ActionResult> CrearTurno(RegistroTurnoDTO dto)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);
                var profesional = await _context.Profesionales.FindAsync(dto.IdProfesional);

                if (paciente == null || profesional == null)
                    return BadRequest("Paciente o profesional inválido.");

                var nuevoTurno = new Turno
                {
                    Comprobante = $"ST-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                    IdPaciente = dto.IdPaciente,
                    IdProfesional = dto.IdProfesional,
                    FechaHora = dto.FechaHora,
                    Duracion = dto.Duracion,
                    Observaciones = dto.Observaciones,
                    IdEstado = 4
                };

                _context.Turnos.Add(nuevoTurno);
                await _context.SaveChangesAsync();


                var turnoDto = _mapper.Map<TurnoDTO>(nuevoTurno);
                return CreatedAtAction(nameof(GetTurno), new { id = nuevoTurno.IdTurno }, turnoDto);


            }
            catch (Exception ex)
            {
                // 👇 esto muestra el error exacto en Swagger o consola
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // PUT: api/Turnos/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<IActionResult> EditarTurno(long id, [FromBody] RegistroTurnoDTO dto)
        {
            try
            {
                var turno = await _context.Turnos.FindAsync(id);

                if (turno == null)
                    return NotFound();

                turno.IdProfesional = dto.IdProfesional;
                turno.FechaHora = dto.FechaHora;
                turno.Duracion = dto.Duracion;
                turno.Observaciones = dto.Observaciones;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        // DELETE: api/turnos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Paciente,Secretaria,Admin")]
        public async Task<IActionResult> CancelarTurno(long id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
                return NotFound();

            turno.IdEstado = 6; // Cancelado
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
