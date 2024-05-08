using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class CrearEspecialidadDTO
    {
        private bool _isActivo;

        public string? NombreEspecialidad { get; set; }
        public bool IsActivo { get => _isActivo; set => _isActivo = value; }

        public CrearEspecialidadDTO()
        {
            _isActivo = true;
        }
    }
}
