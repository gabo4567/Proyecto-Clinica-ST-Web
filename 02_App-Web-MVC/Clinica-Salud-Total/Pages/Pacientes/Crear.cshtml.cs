using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Pacientes
{
    public class CrearModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public CrearModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Paciente { get; set; } = new Paciente();

        public List<Estado> Estados { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Estados = await _context.Estados.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Estados = await _context.Estados.ToListAsync();
                return Page();
            }

            _context.Pacientes.Add(Paciente);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
