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
        private readonly ILogger<PartidaServices> _logger=logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IMapper _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly UOF _uof=uof ?? throw new ArgumentNullException(nameof(uof));
        public async Task<Response<IEnumerable<Partida>>> GetAll()
        {
   
                var partida = await _uof.Partida.GetAll();
           
            return partida;
        }
    }
}
