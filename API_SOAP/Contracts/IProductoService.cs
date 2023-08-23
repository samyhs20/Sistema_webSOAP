using API_SOAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace API_SOAP.Contracts
{
    
    [ServiceContract]
    internal interface IProductoService
    {
        [OperationContract]
        List<Producto> GetAll();

    }
}
