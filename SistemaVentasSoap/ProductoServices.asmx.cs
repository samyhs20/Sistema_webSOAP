using SistemaVentasSoap.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SistemaVentasSoap
{
    /// <summary>
    /// Descripción breve de ProductoServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductoServices : System.Web.Services.WebService
    {
        public readonly ProductoRepository _productoRepository;

        public ProductoServices()
        {
            _productoRepository = new ProductoRepository();
        }

        [WebMethod]
        public List<Producto> ObtenerTodosLosProductos()
        {
            return _productoRepository.GetAll();
        }
        [WebMethod]
        public String CrearProducto(string Descripcion, int IdCategoria, int Stock, decimal Precio)
        {
            Producto producto = new Producto()
            {
                Descripcion = Descripcion,
                IdCategoria = IdCategoria,
                Stock = Stock,
                Precio = Precio
            };
            return _productoRepository.Create(producto);
  
        }
        [WebMethod]
        public Producto BuscarProducto(int Id) 
        { 
              return _productoRepository.BuscarProducto(Id);
        }
        [WebMethod] 
        public String EliminarProducto(int Id)
        {
            return _productoRepository.EliminarProducto(Id);
        }
    }
}
