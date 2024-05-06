using System;
using System.Collections.Generic;

namespace Model.Model;

public partial class TblDetallePartidum
{
    public int IddetallePartida { get; set; }

    public string? CodPartida { get; set; }

    public bool? Adicional { get; set; }

    public decimal? CantProyectada { get; set; }

    public decimal? CantEjecutada { get; set; }

    public decimal? CantFaltante { get; set; }

    public decimal? Porcentaje { get; set; }

    public virtual TblPartidum? CodPartidaNavigation { get; set; }
}
