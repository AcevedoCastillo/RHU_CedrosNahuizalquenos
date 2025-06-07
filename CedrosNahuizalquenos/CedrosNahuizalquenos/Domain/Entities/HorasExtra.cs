using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Domain.Entities;

public partial class HorasExtra
{
    public int HoraExtraId { get; set; }

    public int EmpleadoId { get; set; }

    public DateTime Fecha { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal CantidadHoras { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
