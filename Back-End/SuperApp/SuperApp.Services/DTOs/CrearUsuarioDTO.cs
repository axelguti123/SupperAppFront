using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class CrearUsuarioDTO
    {
        private bool _isActivo;

        public int IdEspecialidad { get; set; }
        public string? Nombre {     get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Nombre_De_Usuario { get; set ; }
        public string? Contraseña { get; set; }
        public bool IsActivo { get => _isActivo; set => _isActivo = value; }

        public CrearUsuarioDTO()
        {
            _isActivo = true;
        }
    }
}
