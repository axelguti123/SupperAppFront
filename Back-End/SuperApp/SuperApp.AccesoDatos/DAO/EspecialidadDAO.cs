using SuperApp.AccesoDatos.Interfaz;
using SupperApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using SuperApp.AccesoDatos.Excepciones;
using SuperApp.AccesoDatos.Utilidades;
namespace SuperApp.AccesoDatos.DAO
{
    internal class EspecialidadDAO : IEspecialidad
    {
        public async Task<Response> Create(Especialidad data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_C_ESPECIALIDAD", cmd =>
            {
                cmd.Parameters.AddWithValue("@nombreEspecialidad", data.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@estado", data.IsActivo);
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
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_D_ESPECIALIDAD", cmd =>
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
            return await DataBaseHelpers.ExecuteReaderAsync("SP_F_ESPECIALIDAD", cmd =>
            {
                cmd.Parameters.AddWithValue("@idEspecialidad",id);
            }, reader =>
            {
                if( reader.Read())
                {
                    return new Especialidad()
                    {
                        IDEspecialidad = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        NombreEspecialidad = reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                    };
                }
                throw new EspecialidadNoEncontradaException("No se pudo encontrar la especialidad");
            });
        }

        public async Task<Response<IEnumerable<Especialidad>>> GetAll()
        {
            return await DataBaseHelpers.ExecuteReaderAsync("SP_R_ESPECIALIDAD", null, reader =>
            {
                var list = new List<Especialidad>();
                while (reader.Read())
                {
                    list.Add(new Especialidad
                    {
                        IDEspecialidad = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        NombreEspecialidad = reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                    });
                }
                return list.AsEnumerable();
            });
        }

        public async Task<Response> Update(Especialidad data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_U_ESPECIALIDAD", cmd =>
            {
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombreEspecialidad", data.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@estado", data.IsActivo);
            });
        }

        

    }
}
