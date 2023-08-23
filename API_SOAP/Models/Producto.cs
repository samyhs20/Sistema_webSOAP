using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace API_SOAP.Models
{
    public class Producto
    {
        [XmlIgnore]
        public int Id { get; set; }
        
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        [XmlIgnore]
        public int Activo { get; set; }
        [XmlIgnore]
        public DateTime FechaRegistro { get; set; }

    }
}