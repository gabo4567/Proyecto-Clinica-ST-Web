using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica_Salud_Total.Models;

public partial class Profesional
{
    [Key]
    public long IdProfesional { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio")]
    [StringLength(20, ErrorMessage = "El DNI no puede superar los 20 caracteres")]
    public string Dni { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "El teléfono no es válido")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
    public string? Telefono { get; set; }

    [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    [StringLength(20, ErrorMessage = "El género no puede superar los 20 caracteres")]
    public string Genero { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    public DateOnly FechaNacimiento { get; set; }

    [Range(0, 150, ErrorMessage = "La edad debe ser un valor válido")]
    public int? Edad { get; set; }

    public string Matricula { get; set; } = null!;

    public long IdEspecialidad { get; set; }

    public long IdEstado { get; set; }

    public virtual ICollection<HorarioDisponible> HorarioDisponibles { get; set; } = new List<HorarioDisponible>();

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
