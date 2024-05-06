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
        public IEspecialidad Especialidad
        {
            get { return _especialidad ??= new EspecialidadDAO(); }
        }
    }
}
