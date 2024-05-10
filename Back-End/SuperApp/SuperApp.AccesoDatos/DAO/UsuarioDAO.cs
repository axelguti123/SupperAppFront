using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Interfaz;
using SupperApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SuperApp.AccesoDatos.DAO
{
    internal class UsuarioDAO : IUsuario
    {
        public async Task<string> Create(Usuario data)
        {
            try
            {
                CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_C_USUARIO", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
                await cmd.ExecuteNonQueryAsync();
                return "Usuario Agregado";
            }catch(SqlException ex){
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                CadenaConexion.Cerrar();
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                CadenaConexion.Abrir();
                using SqlCommand cmd = new("SP_D_USUARIO", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@idUsuario", id);
                await cmd.ExecuteNonQueryAsync();
                return "Registro Eliminado";
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;

            }
            finally
            {
                CadenaConexion.Cerrar();
            }
        }

        public async Task<Usuario> Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            var list=new List<Usuario>();
            try
            {
                CadenaConexion.Abrir();
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
                return list;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                CadenaConexion.Cerrar();
            }
        }

        public async Task<string> Update(Usuario data)
        {
            try
            {
                CadenaConexion.Abrir();
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
                return "Registro Modificado";
            }catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
            finally
            {
                CadenaConexion.Cerrar();
            }
        }
    }
}
