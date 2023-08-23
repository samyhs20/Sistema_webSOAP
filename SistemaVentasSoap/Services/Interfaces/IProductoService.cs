using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasSoap.Services.Interfaces
{
    [ServiceContract]
    internal interface IProductoService
    {
        [OperationContract]
        List<Producto> GetAll();
    }
}
