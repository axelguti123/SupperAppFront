using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Interfaz;
using SupperApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SuperApp.AccesoDatos.DAO
{
    internal class UsuarioDAO : IUsuario
    {
        public string Create(Usuario data)
        {
            try
            {
                CadenaConexion.abrir();
                using (SqlCommand cmd = new("SP_C_USUARIO", CadenaConexion.conectar) { CommandType=CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                    cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                    cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                    cmd.Parameters.AddWithValue("@activo", data.Activo);
                    cmd.ExecuteNonQuery();
                }
                return "Usuario Agregado";
            }catch(SqlException ex){
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                CadenaConexion.cerrar();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            var list=new List<Usuario>();
            try
            {
                CadenaConexion.abrir();
                using(SqlCommand cmd= new("SP_R_USUARIOS", CadenaConexion.conectar) { CommandType = CommandType.StoredProcedure })
                {
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
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
                            Activo = Convert.ToBoolean(reader["activo"])
                        };
                        list.Add(usuario);
                    }
                    return list;
                }
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

        public void Update(Usuario data)
        {
            throw new NotImplementedException();
        }
    }
}
