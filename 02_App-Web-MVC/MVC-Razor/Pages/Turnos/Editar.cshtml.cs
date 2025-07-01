using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V2Razor.Models;

namespace V2Razor.Pages.Turnos
{
    public class EditarModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public EditarModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Turno Turno { get; set; }
        public List<Estado> Estados { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            Estados = await _context.Estados.ToListAsync();

            if (id.HasValue)
            {
                Turno = await _context.Turnos
                    .FirstOrDefaultAsync(p => p.IdTurno == id.Value);

                if (Turno == null)
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
                Estados = await _context.Estados.ToListAsync();
                return Page();
            }

            //Attach avisa a la base de datos modificaciones a entidades.
            _context.Turnos.Attach(Turno).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //comprueba si el Paciente existe.
                if (!TurnoExists(Turno.IdTurno))
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

        private bool TurnoExists(long id)
        {
            // Verifica si existe un Paciente con el Id especificado.
            return _context.Turnos.Any(e => e.IdTurno == id);
        }
    }
}
