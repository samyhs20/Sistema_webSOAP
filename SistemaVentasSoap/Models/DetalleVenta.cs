using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; } 
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }

        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        
    }
}