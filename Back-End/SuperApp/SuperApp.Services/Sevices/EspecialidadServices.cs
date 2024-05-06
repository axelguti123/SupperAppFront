using AutoMapper;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Sevices
{
    internal class EspecialidadServices
    {
        private readonly IMapper _mapper;
        private readonly UOF _uof;

        public EspecialidadServices() { }
        public EspecialidadServices(IMapper mapper, UOF uof)
        {
            _mapper = mapper;
            _uof = uof;
        }

        public IEnumerable<MostrarEspecialidadDTO> GetAll()
        {
            var generos = _uof.Especialidad.GetAll();
            return _mapper.Map<IEnumerable<MostrarEspecialidadDTO>>(generos);
        }
    }
}
