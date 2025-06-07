using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Domain.Entities;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string Area { get; set; } = null!;

    public string PuestoFuncional { get; set; } = null!;

    public decimal SalarioMensual { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<HorasExtra> HorasExtras { get; set; } = new List<HorasExtra>();

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();
}
