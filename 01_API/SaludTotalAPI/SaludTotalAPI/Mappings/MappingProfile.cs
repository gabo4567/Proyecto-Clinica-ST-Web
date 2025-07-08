using AutoMapper;
using SaludTotalAPI.DTOs;
using SaludTotalAPI.Models;

namespace SaludTotalAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));

            CreateMap<Usuario, UsuarioAutenticadoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario));

            CreateMap<RegistroUsuarioDTO, Usuario>();

            // Estado
            CreateMap<Estado, EstadoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEstado));

            // Paciente
            CreateMap<Paciente, PacienteDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPaciente))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));

            CreateMap<Paciente, PacienteMiniDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPaciente));

            // Tutor
            CreateMap<Tutor, TutorDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdTutor))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre))
                .ForMember(dest => dest.Paciente, opt => opt.MapFrom(src => new PacienteMiniDTO
                {
                    Id = src.Paciente.IdPaciente,
                    Nombre = src.Paciente.Nombre,
                    Apellido = src.Paciente.Apellido
                }));

            CreateMap<RegistroTutorDTO, Tutor>();

            // Especialidad
            CreateMap<Especialidad, EspecialidadDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEspecialidad))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));

            // Profesional
            CreateMap<Profesional, ProfesionalDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProfesional))
                .ForMember(dest => dest.Especialidad, opt => opt.MapFrom(src => src.Especialidad.Nombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));

            CreateMap<Profesional, ProfesionalMiniDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProfesional))
                .ForMember(dest => dest.Especialidad, opt => opt.MapFrom(src => src.Especialidad.Nombre));

            // Turno
            CreateMap<Turno, TurnoDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdTurno))
                .ForMember(dest => dest.Paciente, opt => opt.MapFrom(src => new PacienteMiniDTO
                {
                    Id = src.Paciente.IdPaciente,
                    Nombre = src.Paciente.Nombre,
                    Apellido = src.Paciente.Apellido
                }))
                .ForMember(dest => dest.Profesional, opt => opt.MapFrom(src => new ProfesionalMiniDTO
                {
                    Id = src.Profesional.IdProfesional,
                    Nombre = src.Profesional.Nombre,
                    Apellido = src.Profesional.Apellido,
                    Especialidad = src.Profesional.Especialidad.Nombre
                }))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));

            // Horario Disponible
            CreateMap<HorarioDisponible, HorarioDisponibleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdHorario))
                .ForMember(dest => dest.DiaSemana, opt => opt.MapFrom(src => MapDayName(src.DiaSemana)))
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.HoraFin.ToString(@"hh\:mm")))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Nombre));
        }

        private static string MapDayName(int dia)
        {
            return dia switch
            {
                1 => "Lunes",
                2 => "Martes",
                3 => "Miércoles",
                4 => "Jueves",
                5 => "Viernes",
                6 => "Sábado",
                7 => "Domingo",
                _ => "Desconocido"
            };
        }
    }
}

