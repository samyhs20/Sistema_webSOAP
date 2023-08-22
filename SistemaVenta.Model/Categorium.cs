﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Categorium
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
