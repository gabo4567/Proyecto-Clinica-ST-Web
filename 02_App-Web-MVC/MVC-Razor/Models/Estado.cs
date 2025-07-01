using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V2Razor.Models;

public partial class Estado
{
    [Key]
    public long IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Especialidad> Especialidads { get; set; } = new List<Especialidad>();

    public virtual ICollection<HorarioDisponible> HorarioDisponibles { get; set; } = new List<HorarioDisponible>();

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();

    public virtual ICollection<Profesional> Profesionals { get; set; } = new List<Profesional>();

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    public virtual ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
