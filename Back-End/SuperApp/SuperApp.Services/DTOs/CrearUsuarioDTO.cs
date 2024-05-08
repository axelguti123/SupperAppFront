using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.DTOs
{
    internal class CrearUsuarioDTO
    {
        public int IDEspecialidad {  get; set; }
        public string? Nombre { get; set; }
        public string? Apellido {  get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Nombre_de_Usuario {  get; set; }
        public string? Contraseña { get; set; }
        public bool Activo {  get; set; }
    }
}
