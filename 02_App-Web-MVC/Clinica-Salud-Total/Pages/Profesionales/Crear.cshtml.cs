using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Profesionales
{
    public class CrearModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public CrearModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesional Profesional { get; set; } = new Profesional();

        public List<Especialidad> Especialidades { get; set; } = new();
        public List<Estado> Estados { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Especialidades = await _context.Especialidads.ToListAsync();
            Estados = await _context.Estados.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Especialidades = await _context.Especialidads.ToListAsync();
                Estados = await _context.Estados.ToListAsync();
                return Page();
            }

            _context.Profesionals.Add(Profesional);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
} 