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
            CreateMap<Especialidad, MostrarEspecialidadDTO>().ReverseMap();
            CreateMap<Usuario, CrearUsuarioDTO>().ReverseMap();
            CreateMap<Usuario, MostrarUsuarioDTO>().ForMember(dest => dest.NombreEspecialidad,
                opt => opt.MapFrom(mapExpression: src => src.Especialidads.NombreEspecialidad));
            CreateMap<Especialidad, CrearEspecialidadDTO>().ReverseMap();

        }
    }
}
