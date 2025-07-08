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
    public class EstadosController : ControllerBase
    {
        private readonly ClinicaContext _context;
        private readonly IMapper _mapper;

        public EstadosController(ClinicaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstados()
        {
            var estados = await _context.Estados.ToListAsync();
            return Ok(_mapper.Map<List<EstadoDTO>>(estados));
        }
    }
}

