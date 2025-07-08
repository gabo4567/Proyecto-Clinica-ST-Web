using System;
using System.Collections.Generic;

namespace Clinica_Salud_Total.Models;

public partial class HorarioDisponible
{
    public int IdHorario { get; set; }

    public long IdProfesional { get; set; }

    public int DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public long IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
