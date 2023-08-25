using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class Compra: BaseEntity
    {
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public decimal Total { get; set; }

        public int IdUsuario { get; set; }
        //
        public Usuario Usuario { get; set; }
    }
   
}