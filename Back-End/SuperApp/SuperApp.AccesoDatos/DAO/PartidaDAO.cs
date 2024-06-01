using SuperApp.AccesoDatos.Interfaz;
using SuperApp.AccesoDatos.Utilidades;
using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.DAO
{
    internal class PartidaDAO : IPartida
    {
        public Task<Response> Create(Partida data)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Partida>> Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<Partida>>> GetAll()
        {
            return await DataBaseHelpers.ExecuteReaderAsync("SP_R_PARTIDA", null, reader =>
            {
                var list = new List<Partida>();
                while (reader.Read())
                {
                    list.Add(new Partida()
                    {
                        CodPartida = reader.IsDBNull(reader.GetOrdinal("codPartida")) ? "" : reader.GetString(reader.GetOrdinal("codPartida")),
                        partida = reader.IsDBNull(reader.GetOrdinal("partida")) ? "" : reader.GetString(reader.GetOrdinal("partida")),
                        IDEspecialidad = reader.IsDBNull(reader.GetOrdinal("idEspecialidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = reader.IsDBNull(reader.GetOrdinal("nombreEspecialidad")) ? "" : reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                        },
                        Und = reader.IsDBNull(reader.GetOrdinal("Und")) ? "" : reader.GetString(reader.GetOrdinal("Und")),
                        Total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0 : reader.GetDecimal(reader.GetOrdinal("total")),
                        IDPadre = reader.IsDBNull(reader.GetOrdinal("IDPadre")) ? "" : reader.GetString(reader.GetOrdinal("IDPadre")),
                        Nivel = reader.IsDBNull(reader.GetOrdinal("Nivel")) ? 0 : reader.GetInt32(reader.GetOrdinal("Nivel"))
                    });

                }
                return list.AsEnumerable();
            });
           
        }

        private static List<Partida> ArmarJerarquia(IEnumerable<Partida> list)
        {
            var lookup=new Dictionary<string, Partida>();
            foreach (var partida in list)
            {
                lookup[partida.CodPartida]=partida;
                
            }
            List<Partida> raiz = [];
            foreach (var partida in list)
            {
                if (string.IsNullOrEmpty(partida.IDPadre))
                {
                    raiz.Add(partida);
                }else if (lookup.ContainsKey(partida.IDPadre))
                {
                    lookup[partida.IDPadre].ChildPartida.Add(partida);
                }
            }
            return raiz;
        }

        public Task<Response> Update(Partida data)
        {
            throw new NotImplementedException();
        }
    }
}
