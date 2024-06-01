using AutoMapper;
using SuperApp.Services.DTOs;
using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //Mappeo Especialidad
            CreateMap<Especialidad, MostrarEspecialidadDTO>().ReverseMap();
            CreateMap<Especialidad, CrearEspecialidadDTO>().ReverseMap();
            CreateMap<Especialidad, ModificarEspecialidadDTO>().ReverseMap();

            //Mapeo Usuario
            CreateMap<Usuario, CrearUsuarioDTO>().ReverseMap();
            CreateMap<Usuario, MostrarUsuarioDTO>().ForMember(dest => dest.NombreEspecialidad,
                opt => opt.MapFrom(mapExpression: src => src.Especialidads.NombreEspecialidad));
            CreateMap<Usuario, ModificarUsuarioDTO>().ReverseMap();

            //Mapeo Partidas
            CreateMap<Partida,MostrarPartidaDTO>().ForMember(dest => dest.NombreEspecialidad, 
                opt => opt.MapFrom(mapExpression: src => src.Especialidads.NombreEspecialidad))
            .ForMember(dest => dest.childpartida, opt => opt.MapFrom(src => src.ChildPartida))
            .ReverseMap(); ;

            //Otros Mapeos
            CreateMap<Response, ResponseDTO>().ReverseMap();
            CreateMap<Response<Especialidad>, ResponseDTO<MostrarEspecialidadDTO>>().ReverseMap();
            CreateMap<ResponseDTO<IEnumerable<MostrarUsuarioDTO>>, Response<IEnumerable<Usuario>>>().ReverseMap();
            CreateMap<ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>, Response<IEnumerable<Especialidad>>>().ReverseMap();
            CreateMap<Response<Usuario>, ResponseDTO<MostrarUsuarioDTO>>().ReverseMap();
            CreateMap<Response<IEnumerable<Partida>>, ResponseDTO<IEnumerable<MostrarPartidaDTO>>>().ReverseMap();

        }
    }
}
