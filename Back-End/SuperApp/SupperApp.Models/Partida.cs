using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperApp.Models
{
    public class Partida
    {
        public string CodPartida {  get; set; }
        public string partida {  get; set; }
        public int IDEspecialidad {  get; set; }
        public string IDPadre {  get; set; }
        public string Und {  get; set; }
        public decimal Total {  get; set; }
        public int Nivel {  get; set; }
        public virtual Especialidad Especialidads { get; set; }
        public virtual ICollection<Partida> ChildPartida { get; set; }=[];

    }
}
