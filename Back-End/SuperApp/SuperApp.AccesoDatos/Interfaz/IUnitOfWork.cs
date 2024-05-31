using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Interfaz
{
    public interface IUnitOfWork
    {
        IEspecialidad Especialidad { get; }
        IUsuario Usuario { get; }
        IPartida Partida { get; }

    }
}
