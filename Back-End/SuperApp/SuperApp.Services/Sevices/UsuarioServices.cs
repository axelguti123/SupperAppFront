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
                response.Status = "Error";
                response.Message = "Error creando usuario. Ver logs para más detalles.";
            }
            return response;
        }

        public async Task<IEnumerable<MostrarUsuarioDTO>> GetAll()
        {
            try
            {
                var users = await _uof.Usuario.GetAll();
                return _mapper.Map<IEnumerable<MostrarUsuarioDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los usuarios");
                return new List<MostrarUsuarioDTO>(); // Retornar lista vacía en caso de error
            }
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
                response.Status = "Error";
                response.Message = "Error eliminando usuario. Ver logs para más detalles.";
            }
            return response;
        }

        public async Task<MostrarUsuarioDTO> Find(int id)
        {
            try
            {
                var usuario = await _uof.Usuario.Find(id);
                return _mapper.Map<MostrarUsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encontrando usuario con ID {Id}", id);
                return null; // Retornar null en caso de error
            }
        }
    }
}
