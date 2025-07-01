using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V2Razor.Models;

public partial class Turno
{
    [Key]
    public long IdTurno { get; set; }

    [Required(ErrorMessage = "El comprobante es obligatorio.")]
    public string Comprobante { get; set; } = null!;

    [Required(ErrorMessage = "El paciente es obligatorio.")]
    public long IdPaciente { get; set; }

    [Required(ErrorMessage = "El profesional es obligatorio.")]
    public long IdProfesional { get; set; }

    public long? IdTutor { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
    public DateTime FechaHora { get; set; }

    [Required(ErrorMessage = "La duración es obligatoria.")]
    public int Duracion { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public long IdEstado { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaCreacion { get; set; }

    // Propiedades de navegación (cambio a nullible)
    public virtual Estado? IdEstadoNavigation { get; set; }
    public virtual Paciente? IdPacienteNavigation { get; set; }
    public virtual Profesional? IdProfesionalNavigation { get; set; }
    public virtual Tutor? IdTutorNavigation { get; set; }

}
