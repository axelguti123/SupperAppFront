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
                        CodPartida = reader.IsDBNull(reader.GetOrdinal("codPartida")) ? null : reader.GetString(reader.GetOrdinal("codPartida")),
                        partida = reader.IsDBNull(reader.GetOrdinal("partida")) ? null : reader.GetString(reader.GetOrdinal("partida")),
                        IDEspecialidad = reader.IsDBNull(reader.GetOrdinal("idEspecialidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = reader.IsDBNull(reader.GetOrdinal("nombreEspecialidad")) ? null : reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                        },
                        Und = reader.IsDBNull(reader.GetOrdinal("Und")) ? null : reader.GetString(reader.GetOrdinal("Und")),
                        Total = reader.IsDBNull(reader.GetOrdinal("total")) ? 0: reader.GetDecimal(reader.GetOrdinal("total")),
                        IDPadre = reader.IsDBNull(reader.GetOrdinal("IDPadre")) ? null : reader.GetString(reader.GetOrdinal("IDPadre")),
                        Nivel = reader.IsDBNull(reader.GetOrdinal("Nivel")) ? 0 : reader.GetInt32(reader.GetOrdinal("Nivel"))
                    });

                }
                var lista = ArmarJerarquia(list);
                return lista.AsEnumerable();
            });
           
        }

        private static List<Partida> ArmarJerarquia(IEnumerable<Partida> list)
        {
            var partidasLookup = list.ToLookup(p => p.IDPadre);
            foreach (var partida in list)
            {
                partidasLookup[partida.CodPartida]= partida;
            }
            List<Partida> raiz = [];
            foreach (var partida in list)
            {
                if(partida.IDPadre == null)
                {
                    raiz.Add(partida);
                }
                else
                {
                    if (lookup.ContainsKey(partida.IDPadre))
                    {
                        lookup[partida.IDPadre].ChildPartida.Add(partida);
                    }
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
