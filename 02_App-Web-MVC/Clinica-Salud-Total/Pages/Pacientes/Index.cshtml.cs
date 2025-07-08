using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;
using Clinica_Salud_Total.Helpers;

namespace Clinica_Salud_Total.Pages.Pacientes
{
    public class IndexModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public IndexModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public PaginatedList<Paciente> Pacientes { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10; // Cambiar el tamaño
            Pacientes = await PaginatedList<Paciente>.CreateAsync(
                _context.Pacientes
                    .Include(p => p.IdEstadoNavigation)
                    .OrderBy(p => p.Apellido),
                pageIndex ?? 1,
                pageSize
            );
        }
    }
}