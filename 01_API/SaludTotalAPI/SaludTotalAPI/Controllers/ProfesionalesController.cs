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
    [Authorize] // ✅ Solo usuarios autenticados pueden acceder
    public class ProfesionalesController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public ProfesionalesController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/profesionales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesionalDTO>>> GetProfesionales()
        {
            var profesionales = await _context.Profesionales
                .Include(p => p.Especialidad)
                .Include(p => p.Estado)
                .Where(p => p.IdEstado != 2)
                .ToListAsync();

            return Ok(_mapper.Map<List<ProfesionalDTO>>(profesionales));
        }

        // GET: api/profesionales/30000001
        [HttpGet("{dni}")]
        public async Task<ActionResult<ProfesionalDTO>> GetProfesional(string dni)
        {
            var profesional = await _context.Profesionales
                .Include(p => p.Especialidad)
                .Include(p => p.Estado)
                .FirstOrDefaultAsync(p => p.Dni == dni);

            if (profesional == null)
                return NotFound();

            return Ok(_mapper.Map<ProfesionalDTO>(profesional));
        }

        // POST: api/profesionales
        [HttpPost]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<ActionResult> CrearProfesional(Profesional profesional)
        {
            var existeDni = await _context.Profesionales.AnyAsync(p => p.Dni == profesional.Dni);
            if (existeDni)
                return BadRequest("Ya existe un profesional con ese DNI.");

            profesional.IdEstado = 1;
            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesional), new { dni = profesional.Dni }, profesional);
        }

        // PUT: api/profesionales/30000001
        [HttpPut("{dni}")]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<IActionResult> EditarProfesional(string dni, Profesional profesional)
        {
            if (dni != profesional.Dni)
                return BadRequest("El DNI no coincide con el profesional enviado.");

            var existente = await _context.Profesionales.FirstOrDefaultAsync(p => p.Dni == dni);
            if (existente == null)
                return NotFound();

            _mapper.Map(profesional, existente);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/profesionales/30000001
        [HttpDelete("{dni}")]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<IActionResult> EliminarProfesional(string dni)
        {
            var profesional = await _context.Profesionales.FirstOrDefaultAsync(p => p.Dni == dni);
            if (profesional == null)
                return NotFound();

            profesional.IdEstado = 2; // Inactivo
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


