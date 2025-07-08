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
    public class EspecialidadesController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public EspecialidadesController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Especialidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadDTO>>> GetEspecialidades()
        {
            var especialidades = await _context.Especialidades
                .Include(e => e.Estado)
                .Where(e => e.IdEstado == 1)
                .ToListAsync();

            return Ok(_mapper.Map<List<EspecialidadDTO>>(especialidades));
        }
    }
}

