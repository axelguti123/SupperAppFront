using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Excepciones
{
    [Serializable]
    internal class EspecialidadNoEncontradaException(string msg) : SuppException(msg)
    {
    }
}
