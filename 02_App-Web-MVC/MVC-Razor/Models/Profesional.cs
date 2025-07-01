using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V2Razor.Models;

public partial class Profesional
{
    [Key]
    public long IdProfesional { get; set; }

    public string Dni { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string Genero { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int? Edad { get; set; }

    public string Matricula { get; set; } = null!;

    public long IdEspecialidad { get; set; }

    public long IdEstado { get; set; }

    public virtual ICollection<HorarioDisponible> HorarioDisponibles { get; set; } = new List<HorarioDisponible>();

    public virtual Especialidad IdEspecialidadNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
