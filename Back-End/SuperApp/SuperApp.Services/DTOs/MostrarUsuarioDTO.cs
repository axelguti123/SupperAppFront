using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class MostrarUsuarioDTO
    {
        public int IDUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public bool IsActivo { get; set; }
        public int IDEspecialidad { get; set; }

        public string? NombreEspecialidad
        {
            get; set;
        }
    }
}
