using Clinica_Salud_Total.Helpers;
using Clinica_Salud_Total.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Clinica_Salud_Total.Pages.Profesionales
{
    public class IndexModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public IndexModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public PaginatedList<Profesional> Profesionales { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10; // Cambia el tamaño de página si lo deseas
            var profesionalesIQ = _context.Profesionals
                .Include(p => p.IdEspecialidadNavigation)
                .Include(p => p.IdEstadoNavigation)
                .OrderBy(p => p.Apellido);

            Profesionales = await PaginatedList<Profesional>.CreateAsync(
                profesionalesIQ,
                pageIndex ?? 1,
                pageSize
            );
        }
    }
}
