using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Excepciones
{
    [Serializable]
    internal class SuppException:Exception
    {
        public SuppException(string message):base(message) { }
        public SuppException(string message,Exception innerException):base(message,innerException) { }
    }
}
