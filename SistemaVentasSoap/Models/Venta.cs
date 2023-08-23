using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public decimal Total { get; set; }
    }
}