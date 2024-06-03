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
                response = _mapper.Map<ResponseDTO<IEnumerable<MostrarPartidaDTO>>>(partida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los usuarios");
                response.Status = "Error";
                response.Message = ex.Message;
            }
            return response;
            /* var response=new ResponseDTO<IEnumerable<MostrarPartidaDTO>>();
             try
             {
                 var partida = await _uof.Partida.GetAll();
                 if (partida.Status == "Success")
                 {
                     var list = ArmarJerarquia(partida.Data);
                     var mapper = _mapper.Map<IEnumerable<MostrarPartidaDTO>>(list);
                     response.Data=mapper;
                     response.Status=partida.Status;
                     response.Message=partida.Message;
                 }

             }
             catch (Exception ex)
             {
                 response.Status = "Error";
                 response.Message = ex.Message;
             }

             return response;*/
        }
        /*private static List<Partida> ArmarJerarquia(IEnumerable<Partida> list)
        {
            var lookup = new Dictionary<string, Partida>();
            foreach (var partida in list)
            {
                lookup[partida.CodPartida] = partida;

            }
            var raiz = new List<Partida>();
            foreach (var partida in list)
            {
                if (string.IsNullOrEmpty(partida.IDPadre))
                {
                    raiz.Add(partida);
                }
                else if (lookup.TryGetValue(partida.IDPadre, out Partida? value))
                {
                    value.ChildPartida.Add(partida);
                }
            }
            return raiz;
        }*/
    }
}
