using System;
using System.Collections.Generic;

namespace CedrosNahuizalquenos.Domain.Entities;

public partial class UsuariosRrhh
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }
}
