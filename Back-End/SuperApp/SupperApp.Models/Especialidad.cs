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
        private int _iDEspecialidad;
        private string? _nombreEspecialidad;

        public int IDEspecialidad { get => _iDEspecialidad; set => _iDEspecialidad = value; }
        public string? NombreEspecialidad { 
            get => _nombreEspecialidad; 
            set
            {
                if (value != string.Empty)
                {
                    _nombreEspecialidad = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("El nombre de especialidad no debe ser vacio");
                }
            }
        }
        public bool IsActivo{ get => _isActivo; set => _isActivo = value;}

        public Especialidad()
        {
            _isActivo = true;
        }
    }
}
