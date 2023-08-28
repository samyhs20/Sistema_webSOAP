using SistemaVentasSoap.DataAcess;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static SistemaVentasSoap.DataAcess.CarritoRepository;

namespace SistemaVentasSoap
{
    /// <summary>
    /// Descripción breve de VentaServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class VentaServices : System.Web.Services.WebService
    {
        public readonly CarritoRepository _carritoRepository;
        public VentaServices()
        {
            _carritoRepository = new CarritoRepository();
        }
        public class Response 
        {
            public ResultCompra ResCompra { get; set; }
            public List<Result> ResDetalles { get; set; }
        }

        [WebMethod]
        public Response MetodoDeCompraDetalle(Compra compra, List<DetalleCompra> detalles)
        {
            List<Result> resultadosDetalle = new List<Result>();
            ResultCompra resCompra = _carritoRepository.createCompra(compra);
            if (resCompra.Flag)
            {
                int count = 1;
                foreach(var item in detalles)
                {
                    //item.IdVenta = resCompra.Compra.Id;
                    Result resDetalle = _carritoRepository.CreateCompraDetalleCompra(item, resCompra.Compra);
                    count++;
                    resultadosDetalle.Add(resDetalle);
                }
                Response res = new Response()
                {
                    ResCompra = resCompra,
                    ResDetalles = resultadosDetalle
                };
                return res;
            }
            else
            {
                Response res = new Response()
                {
                    ResCompra = null,
                    ResDetalles = null
                };
                return res;


            }
        }

    }
}
