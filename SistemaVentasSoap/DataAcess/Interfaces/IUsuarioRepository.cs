using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    //[ServiceContract]
    internal interface IUsuarioRepository
    {
        //[OperationContract]
        //metodo interfaz obtener todos los usuarios
        List<Usuario> GetAll();
        //[OperationContract]
        //metodo interfaz para crear usuarios
        String Create(Usuario usuario);
        //metodo interfaz para buscar por usuario (LOGIN)
        Usuario GetById(int id);
        bool LoginUser(string username, string pass);
    }
}
