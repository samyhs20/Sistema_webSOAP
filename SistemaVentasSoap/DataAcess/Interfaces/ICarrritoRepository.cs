using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SistemaVentasSoap.DataAcess.CarritoRepository;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    internal interface ICarrritoRepository
    {
        ResultCompra createCompra(Compra compra);
        Result CreateCompraDetalleCompra(DetalleCompra detalle, Compra compra);


    }
}
