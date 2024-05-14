using AutoMapper;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;

namespace SuperApp.Services.Sevices
{
    public class UsuarioServices(IMapper mapper, UOF uof)
    {
        private readonly IMapper _mapper = mapper;
        private readonly UOF _uof = uof;

        public async Task<string> Create(CrearUsuarioDTO userDTO)
        {
            userDTO.Contraseña = Encriptar.Encrypt.EncriptarPassword(userDTO.Contraseña);
            Usuario user = _mapper.Map<Usuario>(userDTO);
            return await _uof.Usuario.Create(user);
        }
        public async Task<IEnumerable<MostrarUsuarioDTO>> GetAll()
        {
            var lst=await _uof.Usuario.GetAll();
            return _mapper.Map<IEnumerable<MostrarUsuarioDTO>>(lst);
        } 
        public async Task<string> Delete(int id)=>await _uof.Usuario.Delete(id);
        public async Task<MostrarUsuarioDTO> Find(int id)
        {
            var usuario = await _uof.Usuario.Find(id);
            return _mapper.Map<MostrarUsuarioDTO>(usuario);
        }

    }
}
