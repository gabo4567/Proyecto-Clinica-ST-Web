using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V2Razor.Models;

namespace V2Razor.Pages.Pacientes
{
    public class IndexModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public IndexModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public IList<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public async Task OnGet()
        {
           Pacientes = await _context.Pacientes
                .Include(p => p.IdEstadoNavigation)
                .ToListAsync();
        }
    }
}
