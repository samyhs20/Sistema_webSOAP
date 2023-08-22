using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Producto
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();

    public virtual Categorium? IdCategoriaNavigation { get; set; }
}
