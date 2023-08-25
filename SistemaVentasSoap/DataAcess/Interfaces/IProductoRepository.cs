using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    //[ServiceContract]
    internal interface IProductoRepository
    {
        //[OperationContract]
        //metodo interfaz obtener todos los prductos
        List<Producto> GetAll();
        //[OperationContract]
        //metodo interfaz para crear producto
        String Create(Producto producto);
        //metodo interfaz para buscar por
        Producto BuscarProducto(int id);
        //metodo para eliminar producto
        String EliminarProducto(int id);
    }
}

internal interface IProductoRepository
    {
    }

