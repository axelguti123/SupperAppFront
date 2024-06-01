using AutoMapper;
using Microsoft.Extensions.Logging;
using SuperApp.AccesoDatos;
using SuperApp.Services.DTOs;
using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Sevices
{
    public class PartidaServices(IMapper mapper,UOF uof, ILogger<PartidaServices> logger)
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly UOF _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        private readonly ILogger<PartidaServices> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public async Task<ResponseDTO<IEnumerable<MostrarPartidaDTO>>> GetAll()
        {
            var response=new ResponseDTO<IEnumerable<MostrarPartidaDTO>>();
            try
            {
                var partida = await _uof.Partida.GetAll();
                var list = ArmarJerarquia(partida.Data);
                response = _mapper.Map<ResponseDTO<IEnumerable<MostrarPartidaDTO>>>(list);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.Message;
            }
            
            return response;
        }
        private static Response<IEnumerable<Partida>> ArmarJerarquia(IEnumerable<Partida> list)
        {
            var lookup = new Dictionary<string, Partida>();
            foreach (var partida in list)
            {
                lookup[partida.CodPartida] = partida;

            }
            Response< List < Partida >> raiz = new Response<List<Partida>>();
            foreach (var partida in list)
            {
                if (string.IsNullOrEmpty(partida.IDPadre))
                {
                    raiz.Data.Add(partida);
                }
                else if (lookup.ContainsKey(partida.IDPadre))
                {
                    lookup[partida.IDPadre].ChildPartida.Add(partida);
                }
            }
            return raiz.Data.AsEnumerable();
        }
    }
}
