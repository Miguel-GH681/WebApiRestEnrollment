using AutoMapper;
using WebApiKalum.Entities;
using WebApiKalum_net_2022.Dtos;

namespace WebApiKalum_net_2022.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(){
            CreateMap<CarreraTecnicaCreateDto,CarreraTecnica>();
            CreateMap<CarreraTecnica, CarreraTecnicaCreateDto>();
            CreateMap<Jornada, JornadaCreateDto>();
            CreateMap<ExamenAdmision, ExamenAdmisionCreateDto>();
            CreateMap<Aspirante, AspiranteListDto>().ConstructUsing(e => new AspiranteListDto{NombreCompleto = $"{e.Apellidos} {e.Nombres}"});
           
            CreateMap<Aspirante, AspiranteDto>().ConstructUsing(e => new AspiranteDto{NombreCompleto = $"{e.Apellidos} {e.Nombres}"});
            CreateMap<Inscripcion, InscripcionesDto>();
            CreateMap<CarreraTecnica, CarreraTecnicaListDto>();
        }
    }
}