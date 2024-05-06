using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Models
{ 
    internal class Especialidad
    {
        private bool _estado;
        public int IDEspecialidad {  get; set; }
        public string nombreEspecialidad { get; set; }
        public bool estado { get { return true; } set { Estado = value; } }

        public bool Estado { 
            get
            {
                _estado = true;
                return _estado;
            } 
            set => _estado = value; 
        }
    }
}
