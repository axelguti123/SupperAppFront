using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Conexion
{
    internal class CadenaConexion
    {

        private static readonly string _conexion = "Data Source=localhost;Initial Catalog=dbObra;Integrated Security=true;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_conexion);
        }
    }
}
