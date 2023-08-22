using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Numerodocumento
{
    public int Id { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
