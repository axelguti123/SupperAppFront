using System;
using System.Collections.Generic;

namespace Model.Model;

public partial class TblUsuario
{
    public int IdUsuario { get; set; }

    public int? IdEspecialidad { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? NombreDeUsuario { get; set; }

    public string? Contraseña { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public virtual TblEspecialidad? IdEspecialidadNavigation { get; set; }
}
