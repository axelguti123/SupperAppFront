using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Excepciones;
using SuperApp.AccesoDatos.Interfaz;
using SuperApp.AccesoDatos.Utilidades;
using SupperApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SuperApp.AccesoDatos.DAO
{
    internal class UsuarioDAO: IUsuario
    {


        public async Task<Response> Create(Usuario data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_C_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
            },
            result =>
            {
                return result switch
                {
                    1 => new Response { Status = "Success", Message = "Registro Eliminado" },
                    -1 => new Response { Status = "Error", Message = "Error. No se encontro una especialidad con el ID proporcionado." },
                    -2 => new Response { Status = "Error", Message = "Error al eliminar especialidad" },
                    _ => new Response { Status = "Error", Message = "Código de retorno no reconocido." }
                };
            });
        }

        public async Task<Response> Delete(int id)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_D_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", id);
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
                    _ => new Response { Status = "Error", Message = "Código de retorno no reconocido." }
                };
            });
            
        }

        public async Task<Response<Usuario>> Find(int id)
        {
            return await DataBaseHelpers.ExecuteReaderAsync<Usuario>("SP_F_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", id);
            }, reader =>
            {
                if (reader.Read())
                {
                    return new Usuario()
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
                }
                throw new UsuarioNoEncontradoException("No se pudo encontrar al usuario");

            });
        }

        public async Task<Response<IEnumerable<Usuario>>> GetAll()
        {
            return await DataBaseHelpers.ExecuteReaderAsync("SP_R_USUARIOS", null, reader =>
            {
                var list = new List<Usuario>();
                while (reader.Read())
                {
                    list.Add(new Usuario()
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
                    });
                }
                return list.AsEnumerable();
            });
        }

        public async Task<Response> Update(Usuario data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_U_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", data.IDUsuario);
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
            });
        }
    }
}
