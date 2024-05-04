using System;
using System.Collections.Generic;

namespace Model.Model;

public partial class TblPartidum
{
    public string CodPartida { get; set; } = null!;

    public string? Partida { get; set; }

    public int? IdEspecialidad { get; set; }

    public string? IdPadre { get; set; }

    public string? Und { get; set; }

    public decimal? Total { get; set; }

    public virtual TblEspecialidad? IdEspecialidadNavigation { get; set; }

    public virtual TblPartidum? IdPadreNavigation { get; set; }

    public virtual ICollection<TblPartidum> InverseIdPadreNavigation { get; set; } = new List<TblPartidum>();

    public virtual ICollection<TblDetallePartidum> TblDetallePartida { get; set; } = new List<TblDetallePartidum>();
}
