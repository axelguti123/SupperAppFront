using System;
using System.Collections.Generic;

namespace Model.Model;

public partial class TblEspecialidad
{
    public int IdEspecialidad { get; set; }

    public string? NombreEspecialidad { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TblPartidum> TblPartida { get; set; } = new List<TblPartidum>();

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
