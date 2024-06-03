using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Excepciones;
using SupperApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace SuperApp.AccesoDatos.Utilidades
{
    internal static class DataBaseHelpers
    {
        private static readonly Dictionary<int, Func<string, Exception>> _ExceptionMapping = new Dictionary<int, Func<string, Exception>>
        {
            {1,(message)=>new UsuarioNoEncontradoException("No se encontro el Usaurio")},
            {2,(message)=>new EspecialidadNoEncontradaException("No se encontro la especialidad") }

        };

        public static async Task<Response> ExecuteNonQueryAsync(string storedProcedure, Action<SqlCommand> action, Func<int, Response> handleReturnValue = null)
        {
            var response = new Response();
            using var connection = CadenaConexion.ObtenerConexion();

            try
            {
                await connection.OpenAsync().ConfigureAwait(false);
                using var cmd = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure };
                action?.Invoke(cmd);
                var returnValue = new SqlParameter
                {
                    ParameterName = "@returnValue",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue,
                };
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                if(handleReturnValue != null)
                {
                    int result=(int)returnValue.Value;
                    return handleReturnValue(result);
                }
                response.Status = "success";
                response.Message = "Operacion Realizada con Exito. ";
            }catch(UsuarioNoEncontradoException ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }catch(EspecialidadNoEncontradaException ex)
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
                await connection.CloseAsync().ConfigureAwait(false);
            }
            return response;
        }

        public static async Task<Response<TEntity>> ExecuteReaderAsync<TEntity>(string storedProcedure, Action<SqlCommand> action, Func<SqlDataReader, TEntity> read)
        {
            var response = new Response<TEntity>();
            using var connection = CadenaConexion.ObtenerConexion();

            try
            {
                await connection.OpenAsync().ConfigureAwait(false);
                using var cmd = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure };
                action?.Invoke(cmd);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                response.Data = read(reader);
                response.Status = "Success";
                response.Message = "Operacion realizada con exito";
            }
            catch (SqlException ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }catch(SqlNullValueException ex)
            {
                response.Status="Error";
                response.Message = ex.Message;
            }
            finally
            {
                await connection.CloseAsync().ConfigureAwait(false);
            }
            return response;
        }
    }
     
}
