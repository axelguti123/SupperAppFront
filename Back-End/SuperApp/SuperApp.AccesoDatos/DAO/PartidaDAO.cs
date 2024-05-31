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
                        CodPartida = reader.GetString(reader.GetOrdinal("codPartida")),
                        partida = reader.GetString(reader.GetOrdinal("partida")),
                        IDEspecialidad = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                        },
                        Und=reader.GetString(reader.GetOrdinal("Und")),
                        Total=reader.GetDecimal(reader.GetOrdinal("total")),
                        IDPadre=reader.GetString(reader.GetOrdinal("IDPadre")),
                        Nivel=reader.GetInt32(reader.GetOrdinal("Nivel"))
                    });

                }
                var lista = ArmarJerarquia(list);
                return lista.AsEnumerable();
            });
           
        }

        private List<Partida> ArmarJerarquia(IEnumerable<Partida> list)
        {
            var partidas = list.ToLookup(p => p.IDPadre);
            foreach(var partida in list)
            {
                if(partida.IDPadre != null)
                {
                    var parent = list.FirstOrDefault(p => p.CodPartida == partida.IDPadre);
                    parent.ChildPartida.Add(partida);
                }
            }
            var rootPartida= list.Where(p=>p.IDPadre==null).ToList();
            return rootPartida;
        }

        public Task<Response> Update(Partida data)
        {
            throw new NotImplementedException();
        }
    }
}
