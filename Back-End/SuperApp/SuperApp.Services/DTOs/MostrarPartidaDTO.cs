using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class MostrarPartidaDTO
    {
        public string CodPartida { get; set; }
        public string partida { get; set; }
        public int IDEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        public string IDPadre { get; set; }
        public string Und { get; set; }
        public decimal Total { get; set; }
        public int Nivel { get; set; }
        public List<MostrarPartidaDTO> childpartida {  get; set; }=[];

    }
}
