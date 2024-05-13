using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Excepciones
{
    [Serializable]
    internal class UsuarioNoEncontradoException:Exception
    {
        public UsuarioNoEncontradoException()
        {

        }

        public UsuarioNoEncontradoException(string message):base(message) { }

    }
}
