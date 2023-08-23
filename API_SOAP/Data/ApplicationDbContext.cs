using API_SOAP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API_SOAP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto> Producto { get; set; }
        //se agrega mas dbs para otros modelos

    }
}