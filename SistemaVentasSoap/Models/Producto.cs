using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SistemaVentasSoap
{
    public class Producto : BaseEntity
    {
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }  
        public Categoria Categoria { get; set; }

    }
}