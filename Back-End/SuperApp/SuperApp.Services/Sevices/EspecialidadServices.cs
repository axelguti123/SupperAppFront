using AutoMapper;
using Microsoft.Extensions.Logging;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;

namespace SuperApp.Services.Sevices
{
    public class EspecialidadServices(IMapper mapper,UOF uof,  ILogger<EspecialidadServices> logger)
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly UOF _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        private readonly ILogger<EspecialidadServices> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>> GetAll()
        {
            var response = new ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>();
            try
            {
                var especialidades = await _uof.Especialidad.GetAll();
                response = _mapper.Map<ResponseDTO<IEnumerable<MostrarEspecialidadDTO>>>(especialidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todas las especialidades");
            }
            return response;
        }

        public async Task<ResponseDTO> Create(CrearEspecialidadDTO especialidadDTO)
        {
            var responseDTO=new ResponseDTO();
            try
            {
                var especialidad= _mapper.Map<Especialidad>(especialidadDTO);
                var response = await _uof.Especialidad.Create(especialidad);
                responseDTO=_mapper.Map<ResponseDTO>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando especialidad");
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var especialidad = await _uof.Especialidad.Delete(id);
                response=_mapper.Map<ResponseDTO>(especialidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando especialidad con ID {Id}", id);
            }
            return response;
        }

        public async Task<ResponseDTO<MostrarEspecialidadDTO>> Find(int id)
        {
            var response = new ResponseDTO<MostrarEspecialidadDTO>();
            try
            {
                var especialidad = await _uof.Especialidad.Find(id);
                response= _mapper.Map<ResponseDTO<MostrarEspecialidadDTO>>(especialidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encontrando especialidad con ID {Id}", id);
            }
            return response;
        }

        public async Task<ResponseDTO> Update(ModificarEspecialidadDTO especialidadDTO)
        {
            var responseDTO = new ResponseDTO();
            try
            {

                var especialidad = _mapper.Map<Especialidad>(especialidadDTO);
                var response = await _uof.Especialidad.Update(especialidad);
                responseDTO = _mapper.Map<ResponseDTO>(response);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error Modificando especialidad");
            }
            return responseDTO;
        }
    }
}
