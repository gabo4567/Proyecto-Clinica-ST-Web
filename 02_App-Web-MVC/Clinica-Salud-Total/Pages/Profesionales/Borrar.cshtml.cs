using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Profesionales
{
    public class BorrarModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public BorrarModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesional Profesional { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id.HasValue)
            {
                Profesional = await _context.Profesionals
                    .Include(p => p.IdEspecialidadNavigation)
                    .FirstOrDefaultAsync(p => p.IdProfesional == id.Value);

                if (Profesional == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Profesional == null)
            {
                return NotFound();
            }

            var profesionalBorrar = await _context.Profesionals.FindAsync(Profesional.IdProfesional);
            if (profesionalBorrar != null)
            {
                _context.Profesionals.Remove(profesionalBorrar);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
} 