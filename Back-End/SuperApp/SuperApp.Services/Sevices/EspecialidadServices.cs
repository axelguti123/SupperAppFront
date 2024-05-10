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
        public async Task<string> Create(CrearEspecialidadDTO especialidad)
        {
            var user=_mapper.Map<Especialidad>(especialidad);
            return await _uof.Especialidad.Create(user);
        }
        public async Task<string> Delete(int id)=>await _uof.Especialidad.Delete(id);
    }
}
