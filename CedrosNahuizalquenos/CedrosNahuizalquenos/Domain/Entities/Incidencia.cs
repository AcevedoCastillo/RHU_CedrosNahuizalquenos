using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Domain.Entities;

public partial class Incidencia
{
    public int IncidenciaId { get; set; }

    public int EmpleadoId { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string? Comentario { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
