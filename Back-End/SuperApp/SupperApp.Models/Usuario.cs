namespace SupperApp.Models
{
    public class Usuario
    {

        public int IDUsuario { get; set; }
        public int IDEspecialidad { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Nombre_de_usuario { get; set; }
        public string? Contraseña { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool IsActivo { get; set; }
        public required Especialidad Especialidads { get; set; }
    }
}
