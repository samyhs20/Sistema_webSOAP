using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SistemaVentasSoap.Models
{
    public class Usuario: BaseEntity
    {

        //[XmlElement("NombresCompleto")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }

        //[XmlElement("Correo")]
        public string Correo { get; set; }
        //[XmlElement("Clave")]
        public string Clave { get; set; }
        //[XmlElement("IdRol")]
        public int IdRol { get; set; }

        public string Username { get; set; }
        //[XmlIgnore]
        //SENTENCIAS PARA HACER REFERENCIA A LAS RELACIONES DE LA TABLA DE DATOS 
        public Rol Rol { get; set; }
    }
}