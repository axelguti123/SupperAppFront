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

        private static string _conexion = "Data Source=localhost;Initial Ctalog=dbObra; Integrated Security=true;";
        public static readonly SqlConnection conectar = new(_conexion);

        public static string Conexion { get => _conexion; set => _conexion = value; }
        public static void abrir()
        {
            if (conectar.State == System.Data.ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void cerrar()
        {
            if (conectar.State == System.Data.ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
