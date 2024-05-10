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

        private static string _conexion = "Data Source=localhost;Initial Catalog=dbObra; Integrated Security=true;";
        protected internal static readonly SqlConnection conectar = new(_conexion);

        public static string Conexion { get => _conexion; set => _conexion = value; }
        public static void Abrir()
        {
            if (conectar.State == System.Data.ConnectionState.Closed)
            {
                conectar.OpenAsync();
            }
        }
        public static void Cerrar()
        {
            if (conectar.State == System.Data.ConnectionState.Open)
            {
                conectar.CloseAsync();
            }
        }
    }
}
