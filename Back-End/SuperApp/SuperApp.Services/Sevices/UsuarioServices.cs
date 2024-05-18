using AutoMapper;
using Microsoft.Extensions.Logging;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;

namespace SuperApp.Services.Sevices
{
    public class UsuarioServices(IMapper mapper, UOF uof, ILogger<UsuarioServices> logger)
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly UOF _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        private readonly ILogger<UsuarioServices> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<ResponseDTO> Create(CrearUsuarioDTO userDTO)
        {
            var response = new ResponseDTO();
            try
            {
                userDTO.Contraseña = Encriptar.Encrypt.EncriptarPassword(userDTO.Contraseña);
                var user = _mapper.Map<Usuario>(userDTO);
                var uofResponse = await _uof.Usuario.Create(user);
                response = _mapper.Map<ResponseDTO>(uofResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando usuario");
            }
            return response;
        }

        public async Task<ResponseDTO<IEnumerable<MostrarUsuarioDTO>>> GetAll()
        {
            var response=new ResponseDTO<IEnumerable<MostrarUsuarioDTO>>();
            try
            {
                var users = await _uof.Usuario.GetAll();
                response = _mapper.Map<ResponseDTO<IEnumerable<MostrarUsuarioDTO>>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los usuarios");
                response.Status = "Error";
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var uofResponse = await _uof.Usuario.Delete(id);
                response = _mapper.Map<ResponseDTO>(uofResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando usuario con ID {Id}", id);
            }
            return response;
        }

        public async Task<ResponseDTO<MostrarUsuarioDTO>> Find(int id)
        {
            var responseDTO=new ResponseDTO<MostrarUsuarioDTO>();
            try
            {
                var usuario = await _uof.Usuario.Find(id);
                responseDTO= _mapper.Map<ResponseDTO<MostrarUsuarioDTO>> (usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encontrando usuario con ID {Id}", id);
                responseDTO.Status = "Error";
                responseDTO.Message = ex.Message;
            }
            return responseDTO;
        }
        public async Task<ResponseDTO> Update(ModificarUsuarioDTO user)
        {
            var responseDTO = new ResponseDTO();
            try
            {
                var usuario = _mapper.Map<Usuario>(user);
                var response=await _uof.Usuario.Update(usuario);
                responseDTO=_mapper.Map<ResponseDTO>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Modificando Especailidad");
                responseDTO.Status="Error";
                responseDTO.Message= ex.Message;
            }
            return responseDTO;
        }
    }
}
