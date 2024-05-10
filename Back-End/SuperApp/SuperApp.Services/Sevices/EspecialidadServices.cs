using AutoMapper;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;

namespace SuperApp.Services.Sevices
{
    public class EspecialidadServices(IMapper mapper, UOF uof)
    {
        private readonly IMapper _mapper = mapper;
        private readonly UOF _uof = uof;

        public IEnumerable<MostrarEspecialidadDTO> GetAll()
        {
            var especialidad = _uof.Especialidad.GetAll();
            return _mapper.Map<IEnumerable<MostrarEspecialidadDTO>>(especialidad);
        }
        public string Create(CrearEspecialidadDTO especialidad)
        {
            var user=_mapper.Map<Especialidad>(especialidad);
            return _uof.Especialidad.Create(user);
        }
        public string Delete(int id)=>_uof.Especialidad.Delete(id);
    }
}
