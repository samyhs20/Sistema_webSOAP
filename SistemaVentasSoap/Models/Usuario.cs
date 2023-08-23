using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SistemaVentasSoap.Models
{
    public class Usuario
    {
        //[XmlIgnore]
        public int Id { get; set; }
        public string NombresCompleto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }
       // [XmlIgnore]
        public Rol Rol { get; set; }
    }
}