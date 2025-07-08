 using System;
using System.Collections.Generic;

namespace Clinica_Salud_Total.Models;

public partial class Usuario
{
    public long IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public long IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}
