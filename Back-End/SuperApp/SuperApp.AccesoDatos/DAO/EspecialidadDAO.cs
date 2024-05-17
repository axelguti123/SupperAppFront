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
using SuperApp.AccesoDatos.Excepciones;

namespace SuperApp.AccesoDatos.DAO
{
    internal class EspecialidadDAO : IEspecialidad
    {
        public async Task<string> Create(Especialidad data)
        {
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_C_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@nombreEspecialidad", data.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@estado", data.IsActivo);
                await cmd.ExecuteNonQueryAsync();
                return "Especialidad Agregada";
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_D_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idEspecialidad", id);
                await cmd.ExecuteNonQueryAsync();
                return "Registro Eliminado";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
        }

        public async Task<Especialidad> Find(int id)
        {
            try
            {
                Especialidad especialidad;
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_F_ESPECIALIDAD", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@idEspecialidad",id);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    especialidad = new Especialidad()
                    {
                        IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                        NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"]),
                    };
                    return especialidad;
                }
                throw new EspecialidadNoEncontradaException("No se pudo encontrar El Registro");
            }
            catch (EspecialidadNoEncontradaException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
        }

        public async Task<IEnumerable<Especialidad>> GetAll()
        {
            var list = new List<Especialidad>();
            try
            {
                await CadenaConexion.Abrir();
                using (SqlCommand cmd = new("SP_R_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure })
                {

                    using SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false) )
                        {
                            Especialidad especialidad = new()
                            {
                                IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                                NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"]),
                            };
                            list.Add(especialidad);
                        }
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
                await CadenaConexion.Cerrar();
            }
        }

        public async Task<string> Update(Especialidad data)
        {
            try
            {
                await CadenaConexion.Abrir();
                using(SqlCommand cmd =new("SP_D_USUARIO", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure})
                {
                    cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                    cmd.Parameters.AddWithValue("@nombreEspecialidad",data.NombreEspecialidad);
                    cmd.Parameters.AddWithValue("@estado",data.IsActivo);
                    await cmd.ExecuteNonQueryAsync();
                    return "Registro Modificado";
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
        }
    }
}
