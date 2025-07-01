using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V2Razor.Models;

namespace V2Razor.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public IndexModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public IList<Turno> Turnos { get; set; } = new List<Turno>();
        public async Task OnGet()
        {
            Turnos = await _context.Turnos
                 .Include(p => p.IdEstadoNavigation)
                 .ToListAsync();
        }
    }
}
