using SuperApp.AccesoDatos.DAO;
using SuperApp.AccesoDatos.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos
{
    public class UOF : IUnitOfWork
    {
        private IEspecialidad? _especialidad;
        private IUsuario? _usuario;
        private IPartida? _partida;
        public IEspecialidad Especialidad
        {
            get { return _especialidad ??= new EspecialidadDAO(); }
        }

        public IUsuario Usuario { get { return _usuario ??= new UsuarioDAO(); } }

        public IPartida Partida { get { return _partida ??= new PartidaDAO(); } }
    }
}
