using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static SistemaVentasSoap.DataAcess.UsuarioRepository;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    //[ServiceContract]
    internal interface IUsuarioRepository
    {
        //[OperationContract]
        //metodo interfaz obtener todos los usuarios
        ResultArray GetAll();
        //[OperationContract]
        //metodo interfaz para crear usuarios
        Result Create(Usuario usuario);
        //metodo interfaz para buscar por usuario (LOGIN)
        Result GetById(int id);
        ResultLogin LoginUser(string username, string pass);

        String Delete(int id);
    }
}
