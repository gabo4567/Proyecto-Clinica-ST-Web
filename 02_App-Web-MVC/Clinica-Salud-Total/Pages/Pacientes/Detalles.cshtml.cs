using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Pacientes
{
    public class DetallesModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public DetallesModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        public Paciente Paciente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Paciente = await _context.Pacientes
                .Include(p => p.IdEstadoNavigation)
                .FirstOrDefaultAsync(p => p.IdPaciente == id);

            if (Paciente == null)
                return NotFound();

            return Page();
        }
    }
}
