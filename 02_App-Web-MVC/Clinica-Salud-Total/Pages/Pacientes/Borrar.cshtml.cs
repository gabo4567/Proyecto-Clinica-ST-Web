using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Clinica_Salud_Total.Models;

namespace Clinica_Salud_Total.Pages.Pacientes
{
    public class BorrarModel : PageModel
    {
        private readonly ClinicaSaludTotalContext _context;

        public BorrarModel(ClinicaSaludTotalContext context)
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
            if (Paciente == null)
            {
                return NotFound();
            }

            var pacienteBorrar = await _context.Pacientes.FindAsync(Paciente.IdPaciente);
            if (pacienteBorrar != null)
            {
                // Buscar tutores relacionados
                var tutoresRelacionados = _context.Tutors.Where(t => t.IdPaciente == pacienteBorrar.IdPaciente).ToList();

                // Eliminar turnos de cada tutor relacionado
                foreach (var tutor in tutoresRelacionados)
                {
                    var turnosTutor = _context.Turnos.Where(turno => turno.IdTutor == tutor.IdTutor);
                    _context.Turnos.RemoveRange(turnosTutor);
                }

                // Eliminar tutores relacionados
                _context.Tutors.RemoveRange(tutoresRelacionados);

                // Eliminar paciente
                _context.Pacientes.Remove(pacienteBorrar);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
