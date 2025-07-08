using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;
using Clinica_Salud_Total.Helpers;

namespace Clinica_Salud_Total.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public IndexModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public PaginatedList<Turno> Turnos { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10;
            var turnosIQ = _context.Turnos
                .Include(t => t.IdEstadoNavigation)
                .OrderByDescending(t => t.FechaHora);

            Turnos = await Clinica_Salud_Total.Helpers.PaginatedList<Turno>.CreateAsync(
                turnosIQ,
                pageIndex ?? 1,
                pageSize
            );
        }
    }
}
