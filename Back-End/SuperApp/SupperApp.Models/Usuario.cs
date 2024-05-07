namespace SupperApp.Models
{
    public class Usuario
    {
        private bool _activo;
        private DateTime _fechaCreacion;
        private int _iDUsuario;
        private int _iDEspecialidad;
        private DateTime _fechaNacimiento;
        public Usuario()
        {
            _activo = false;
        }

        public int IDUsuario { get => _iDUsuario; set => _iDUsuario = value; }

        public int IDEspecialidad { get => _iDEspecialidad; set => _iDEspecialidad = value; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public string? Nombre_de_usuario { get; set; }
        public string? Contraseña { get; set; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public Especialidad? Especialidads { get; set; }
    }
}
