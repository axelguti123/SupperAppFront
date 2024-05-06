using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperApp.Models
{
    public class Especialidad
    {
        private bool _isActivo;
        public int IDEspecialidad { get; set; }
        public string? NombreEspecialidad { get; set; }
        public bool IsActivo
        {
            get => _isActivo;
            set => _isActivo = value;
        }

        public Especialidad()
        {
            _isActivo = true;
        }
    }
}
