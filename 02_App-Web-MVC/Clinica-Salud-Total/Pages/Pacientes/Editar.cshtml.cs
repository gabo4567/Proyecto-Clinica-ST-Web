using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Pacientes
{
    public class EditarModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public EditarModel(ClinicaSaludTotalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Paciente { get; set; }
        public List<Estado> Estados { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            Estados = await _context.Estados.ToListAsync();

            if (id.HasValue)
            {
                Paciente = await _context.Pacientes
                    .FirstOrDefaultAsync(p => p.IdPaciente == id.Value);

                if (Paciente == null)
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
            _context.Pacientes.Attach(Paciente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //comprueba si el Paciente existe.
                if (!PacienteExists(Paciente.IdPaciente))
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

        private bool PacienteExists(long id)
        {
            // Verifica si existe un Paciente con el Id especificado.
            return _context.Pacientes.Any(e => e.IdPaciente == id);
        }
    }
}
