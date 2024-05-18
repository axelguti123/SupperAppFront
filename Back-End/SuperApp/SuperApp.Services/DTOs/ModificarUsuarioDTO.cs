using SupperApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    public class ModificarUsuarioDTO
    {
        public int IDUsuario { get; set; }
        public int IDEspecialidad { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Nombre_de_usuario { get; set; }
        public string? Contraseña { get; set; }
        public bool IsActivo { get; set; }
    }
}
