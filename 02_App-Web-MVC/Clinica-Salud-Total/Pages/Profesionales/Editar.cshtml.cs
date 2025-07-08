using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Profesionales
{
    public class EditarModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public EditarModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesional Profesional { get; set; }
        public List<Especialidad> Especialidades { get; set; } = new();
        public List<Estado> Estados { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            Especialidades = await _context.Especialidads.ToListAsync();
            Estados = await _context.Estados.ToListAsync();

            if (id.HasValue)
            {
                Profesional = await _context.Profesionals
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
            if (!ModelState.IsValid)
            {
                Especialidades = await _context.Especialidads.ToListAsync();
                Estados = await _context.Estados.ToListAsync();
                return Page();
            }

            //Attach avisa a la base de datos modificaciones a entidades.
            _context.Profesionals.Attach(Profesional).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //comprueba si el Profesional existe.
                if (!ProfesionalExists(Profesional.IdProfesional))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }

        private bool ProfesionalExists(long id)
        {
            // Verifica si existe un Profesional con el Id especificado.
            return _context.Profesionals.Any(e => e.IdProfesional == id);
        }
    }
} 