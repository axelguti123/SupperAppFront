using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Interfaz
{
    internal interface IUnitOfWork
    {
        IEspecialidad Especialidad { get; }

    }
}
