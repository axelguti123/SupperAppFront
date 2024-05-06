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
    internal class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Especialidad, MostrarEspecialidadDTO>().ReverseMap();
        }
    }
}
