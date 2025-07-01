using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace V2Razor.Models;

public partial class Usuario
{
    [Key]
    public long IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public long IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}
