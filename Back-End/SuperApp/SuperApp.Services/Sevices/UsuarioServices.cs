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
        public IEnumerable<MostrarUsuarioDTO> GetAll()
        {
            var lst=_uof.Usuario.GetAll();
            return _mapper.Map<IEnumerable<MostrarUsuarioDTO>>(lst);
        } 
        public async Task<string> Delete(int id)=>await _uof.Usuario.Delete(id);

    }
}
