using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.DTOs;

namespace SaludTotalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HorariosDisponiblesController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public HorariosDisponiblesController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HorariosDisponibles
        [HttpGet]
        [Authorize(Roles = "Admin,Secretaria")]
        public async Task<ActionResult<IEnumerable<HorarioDisponibleDTO>>> GetTodos()
        {
            var horarios = await _context.HorariosDisponibles
                .Include(h => h.Estado)
                .ToListAsync();

            return Ok(_mapper.Map<List<HorarioDisponibleDTO>>(horarios));
        }

        // GET: api/HorariosDisponibles/profesional/3
        [HttpGet("profesional/{idProfesional}")]
        public async Task<ActionResult<IEnumerable<HorarioDisponibleDTO>>> GetPorProfesional(long idProfesional)
        {
            var horarios = await _context.HorariosDisponibles
                .Include(h => h.Estado)
                .Where(h => h.IdProfesional == idProfesional && h.IdEstado != 2) // solo activos
                .ToListAsync();

            return Ok(_mapper.Map<List<HorarioDisponibleDTO>>(horarios));
        }
    }
}

