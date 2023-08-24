using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Models
{
    public class MenuRol: BaseEntity
    {
        
        public int IdMenu { get; set; }
        public int IdRol { get; set; }  

        public Menu Menu { get; set; }
        public Rol Rol { get; set; }
    }
}