using AutoMapper;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SuperApp.Services.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Sevices
{
    public class EspecialidadServices(IMapper mapper, UOF uof) : IEspecialidadServices
    {
        private readonly IMapper _mapper = mapper;
        private readonly UOF _uof = uof;

        public IEnumerable<MostrarEspecialidadDTO> GetAll()
        {
            var generos = _uof.Especialidad.GetAll();
            return _mapper.Map<IEnumerable<MostrarEspecialidadDTO>>(generos);
        }
    }
}
