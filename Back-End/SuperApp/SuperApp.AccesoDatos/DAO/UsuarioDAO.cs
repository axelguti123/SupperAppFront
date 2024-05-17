using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Excepciones;
using SuperApp.AccesoDatos.Interfaz;
using SupperApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SuperApp.AccesoDatos.DAO
{
    internal class UsuarioDAO: IUsuario
    {


        public async Task<Response> Create(Usuario data)
        {
            var response=new Response();
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_C_USUARIO", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
                await cmd.ExecuteNonQueryAsync();
                response.Status = "Success";
                response.Message = "Registro Agregado";
            }catch(SqlException ex){
                response.Status = "Error";
                response.Message = ex.Message;
            }
            finally
            {
                await CadenaConexion.Cerrar();
            }
            return response;
        }

        public async Task<Response> Delete(int id)
        {
            var response=new Response();
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_D_USUARIO", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idUsuario", id);
                await cmd.ExecuteNonQueryAsync();
                response.Status = "Success";
                response.Message = "Registro Eliminado";
            }catch(Exception ex)
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

        public async Task<Response<Usuario>> Find(int id)
        {
            var response=new Response<Usuario>();
            try
            {
                Usuario user;
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_F_USUARIO", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idUsuario", id);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {

                    while(await reader.ReadAsync())
                    {
                        user = new()
                        {
                            IDUsuario = reader.GetInt32(0),
                            IDEspecialidad = reader.GetInt32(1),
                            Especialidads = new Especialidad()
                            {
                                NombreEspecialidad = Convert.ToString(reader.GetInt32(2))
                            },
                            Nombre = Convert.ToString(reader.GetInt32(3)),
                            Apellido = Convert.ToString(reader.GetInt32(4)),
                            IsActivo = Convert.ToBoolean(reader.GetInt32(5))
                        };
                        response.Data = user;
                    }
                    response.Status = "Success";
                    response.Message = "Registro Encontrado";

                }
                else
                {

                    throw new UsuarioNoEncontradoException("Usuario No encontrado");
                }
                reader.Close();

            }
            catch (UsuarioNoEncontradoException ex)
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

        public async Task<Response<IEnumerable<Usuario>>> GetAll()
        {
            var response= new Response<IEnumerable<Usuario>>();
            var list=new List<Usuario>();
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_R_USUARIOS", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Usuario usuario = new()
                    {
                        IDUsuario = Convert.ToInt32(reader["idUsuario"]),
                        IDEspecialidad = Convert.ToInt32(reader["idEspecialidad"]),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = Convert.ToString(reader["nombreEspecialidad"])
                        },
                        Nombre = Convert.ToString(reader["nombre"]),
                        Apellido = Convert.ToString(reader["Apellido"]),
                        IsActivo = Convert.ToBoolean(reader["activo"])
                    };
                    list.Add(usuario);
                }
                response.Data = list;
                response.Status = "Success";
                response.Message = "Datos Recuperados";
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

        public async Task<Response> Update(Usuario data)
        {
            var response = new Response();
            try
            {
                await CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_U_USUARIO", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@idUsuario",data.IDUsuario);
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
                await cmd.ExecuteNonQueryAsync();
                response.Status = "Success";
                response.Message = "Registro Agregado";
            }catch (Exception ex)
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
    }
}
