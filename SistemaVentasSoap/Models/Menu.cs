using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class Menu:BaseEntity
    {
       
        public string Nombre { get; set; }    
        public string Icono { get; set; }
        public int Url { get; set; }
    }
}