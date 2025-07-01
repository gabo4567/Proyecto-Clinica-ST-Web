using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V2Razor.Models;

public partial class Especialidad
{
    [Key]
    public long IdEspecialidad { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public long IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Profesional> Profesionals { get; set; } = new List<Profesional>();
}
