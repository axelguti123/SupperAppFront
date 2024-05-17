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
using System.Reflection.PortableExecutable;

namespace SuperApp.AccesoDatos.DAO
{
    internal class EspecialidadDAO : IEspecialidad
    {
        public async Task<Response> Create(Especialidad data)
        {
            return await ExecuteNonQueryAsync("SP_C_ESPECIALIDAD", cmd =>
            {
                cmd.Parameters.AddWithValue("@nombreEspecialidad", data.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@estado", data.IsActivo);
            });
        }

        public async Task<Response> Delete(int id)
        {
            return await ExecuteNonQueryAsync("SP_D_ESPECIALIDAD", cmd =>
            {
                cmd.Parameters.AddWithValue("@idEspecialidad", id);
                var returnVlue = new SqlParameter
                {
                    ParameterName = "@returnValue",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue,
                };
                cmd.Parameters.Add(returnVlue);
            },
            result =>
            {
                return result switch
                {
                    1 => new Response { Status = "Success", Message = "Registro Eliminado" },
                    -1 => new Response { Status = "Error", Message = "Error. No se encontro una especialidad con el ID proporcionado." },
                    -2 => new Response { Status = "Error", Message = "Error al eliminar especialidad" },
                    _ => new Response  { Status = "Error", Message = "Código de retorno no reconocido."}
                };
            });
        }

        public async Task<Response<Especialidad>> Find(int id)
        {
            var response=new Response<Especialidad>();
            try
            {
                Especialidad especialidad;
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_F_ESPECIALIDAD", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@idEspecialidad",id);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if(reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        especialidad = new Especialidad()
                        {
                            IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                            NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"]),
                        };
                        response.Data = especialidad;
                    }
                    response.Status = "Success";
                    response.Message = "Registro Encontrado";
                }
                else
                {
                    throw new EspecialidadNoEncontradaException("No se pudo encontrar El Registro");

                }
                
            }
            catch (EspecialidadNoEncontradaException ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }

        public async Task<Response<IEnumerable<Especialidad>>> GetAll()
        {
            var response=new Response<IEnumerable<Especialidad>>();
            var list = new List<Especialidad>();
            try
            {
                await CadenaConexion.Abrir();
                using (SqlCommand cmd = new("SP_R_ESPECIALIDAD", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure })
                {
<<<<<<< HEAD

                    using SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false) )
=======
                    using SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
>>>>>>> 19524fb444718f9e27415dd392933bad66dcf271
                        {
                            Especialidad especialidad = new()
                            {
                                IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                                NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"]),
                            };
                            list.Add(especialidad);
                        }
<<<<<<< HEAD
                    }   
=======
                    }
                    reader.Close();
>>>>>>> 19524fb444718f9e27415dd392933bad66dcf271
                }
                response.Data = list;
                response.Status = "Success";
                response.Message = "Datos recuperados";
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.StackTrace;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }

        public async Task<Response> Update(Especialidad data)
        {
            var response=new Response();
            try
            {
                await CadenaConexion.Abrir();
                using(SqlCommand cmd =new("SP_D_USUARIO", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure})
                {
                    cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                    cmd.Parameters.AddWithValue("@nombreEspecialidad",data.NombreEspecialidad);
                    cmd.Parameters.AddWithValue("@estado",data.IsActivo);
                    await cmd.ExecuteNonQueryAsync();
                    response.Status = "Success";
                    response.Message = "Registro Modificado";
                }
            }catch(Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }

        private async Task<Response> ExecuteNonQueryAsync(string storedProcedure,Action<SqlCommand> action)
        {
            var response = new Response();
            try
            {
                await CadenaConexion.Abrir();
                using var cmd = new SqlCommand(storedProcedure, CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                action.Invoke(cmd);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                response.Status = "success";
                response.Message = "Operacion Realizada con Exito. ";
            }
            catch(SqlException ex)
            {
                response.Status="Error"; 
                response.Message = ex.Message;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }

        private async Task<Response<TEntity>> ExecuteReaderAsync<TEntity>(string storedProcedure,Action<SqlCommand> action, Func<SqlDataReader,TEntity> read)
        {
            var response=new Response<TEntity>();
            try
            {
                await CadenaConexion.Abrir();
                using var cmd = new SqlCommand(storedProcedure, CadenaConexion.conectar) { CommandType= CommandType.StoredProcedure };
                action.Invoke(cmd);
                using var reader=await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                response.Data=read(reader);
                response.Status = "success";
                response.Message = "Operacion realizada con exito";
            }catch(EspecialidadNoEncontradaException ex)
            {
                response.Status="Error";
                response.Message=ex.Message;
            }catch(SqlException ex)
            {
                response.Status="Error";
                response.Message=ex.Message;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }
    }
}
