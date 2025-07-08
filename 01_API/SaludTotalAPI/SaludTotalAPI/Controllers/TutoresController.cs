using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;
using SaludTotalAPI.Models;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public TutoresController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/tutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorDTO>>> GetTutores()
        {
            var tutores = await _context.Tutores
                .Include(t => t.Paciente)
                .Include(t => t.Estado)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TutorDTO>>(tutores));
        }

        // POST: api/tutores
        [HttpPost]
        public async Task<ActionResult> CrearTutor(RegistroTutorDTO dto)
        {
            var tutor = _mapper.Map<Tutor>(dto);
            tutor.IdEstado = 1; // Activo

            tutor.Edad = DateTime.Today.Year - dto.FechaNacimiento.Year;
            if (dto.FechaNacimiento.Date > DateTime.Today.AddYears(-tutor.Edad.Value))
                tutor.Edad--;

            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE lógico: api/tutores/50000001
        [HttpDelete("{dni}")]
        public async Task<IActionResult> EliminarTutor(string dni)
        {
            var tutor = await _context.Tutores.FirstOrDefaultAsync(t => t.Dni == dni);
            if (tutor == null)
                return NotFound();

            tutor.IdEstado = 2; // Inactivo
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

