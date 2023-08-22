using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Ventum
{
    public int Id { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();
}
