using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class Venta: BaseEntity
    {
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public decimal Total { get; set; }

        public int IdCliente { get; set; }
        //
        public Cliente Cliente { get; set; }
    }
   
}