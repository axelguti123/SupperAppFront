using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Interfaz;
using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.DAO
{
    internal class EspecialidadDAO : IEspecialidad
    {
        public Especialidad Create(Especialidad data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Especialidad Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Especialidad> GetAll()
        {
            var list = new List<Especialidad>();
            try
            {
                CadenaConexion.abrir();
                using (SqlCommand cmd = new SqlCommand("SP_R_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure })
                {
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Especialidad especialidad = new()
                        {
                            IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                            NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"]),
                        };
                        list.Add(especialidad);
                    }
                }


                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                CadenaConexion.cerrar();
            }
        }

        public void Update(Especialidad data)
        {
            throw new NotImplementedException();
        }
    }
}
