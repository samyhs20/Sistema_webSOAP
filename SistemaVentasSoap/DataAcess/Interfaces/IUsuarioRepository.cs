using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    [ServiceContract]
    internal interface IUsuarioRepository
    {
        [OperationContract]
        List<Usuario> GetAll();
        [OperationContract]
        String Create(Usuario usuario);
        Usuario GetById(int id);
        bool LoginUser(string username, string pass);
    }
}
