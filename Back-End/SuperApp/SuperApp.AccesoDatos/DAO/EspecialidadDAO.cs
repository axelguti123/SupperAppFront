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
        public string Create(Especialidad data)
        {
            try
            {
                CadenaConexion.abrir();
                using SqlCommand cmd = new("SP_C_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@nombreEspecialidad", data.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@estado", data.IsActivo);
                cmd.ExecuteNonQuery();
                return "Especialidad Agregada";
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                CadenaConexion.cerrar();
            }
        }

        public string Delete(int id)
        {
            try
            {
                CadenaConexion.abrir();
                using SqlCommand cmd = new("SP_D_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idEspecialidad", id);
                cmd.ExecuteNonQuery();
                return "Registro Eliminado";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                CadenaConexion.cerrar();
            }
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

        public string Update(Especialidad data)
        {
            throw new NotImplementedException();
        }
    }
}
