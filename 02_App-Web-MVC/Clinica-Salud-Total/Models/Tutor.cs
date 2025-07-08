using System;
using System.Collections.Generic;

namespace Clinica_Salud_Total.Models;

public partial class Tutor
{
    public long IdTutor { get; set; }

    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string Genero { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int? Edad { get; set; }

    public string? ObraSocial { get; set; }

    public string? Parentesco { get; set; }

    public long IdPaciente { get; set; }

    public long IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
