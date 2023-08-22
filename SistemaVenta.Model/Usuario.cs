using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string? NombresCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public int? IdRol { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
