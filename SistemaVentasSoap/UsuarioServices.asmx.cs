using SistemaVentasSoap.DataAcess;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static SistemaVentasSoap.DataAcess.UsuarioRepository;

namespace SistemaVentasSoap
{
    /// <summary>
    /// Descripción breve de UsuarioServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class UsuarioServices : System.Web.Services.WebService
    {
        public readonly UsuarioRepository _usuarioRepository;

        public UsuarioServices()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [WebMethod]
        public ResultArray ObtenerTodosLosUsuarios()
        {
            return _usuarioRepository.GetAll();
        }


        [WebMethod]
        public Result InsertarUsuario(string nombre, string username ,string apellido, string direccion, string telefono, int edad, string correo, int idRol, string clave)
        {
            Usuario usuario = new Usuario()
            {

                Nombre = nombre,
                Apellido = apellido,
                Direccion = direccion,
                Telefono = telefono, 
                Edad = edad,
                Correo = correo,
                Clave = clave,
                IdRol = idRol,
                Username = username

            };
            return _usuarioRepository.Create(usuario);
        }

        [WebMethod]
        public Result BuscarUsuario(int id)
        {
            return _usuarioRepository.GetById(id);
        }
        [WebMethod]
        public ResultLogin LoginUsuario(string username, string clave)
        {
            return _usuarioRepository.LoginUser(username, clave);
        }
        [WebMethod]
        public String EliminarUsuario(int id)
        {
            return _usuarioRepository.Delete(id);
        }

        [WebMethod]
        public Result ActualizarUsuario(int id, string nombre, string username, string apellido, string direccion, string telefono, int edad, string correo, int idRol, string clave)
        {

            ResultArray result = _usuarioRepository.GetAll();
            bool uniqueuser = false; // Inicialización predeterminada

            foreach (var item in result.Usuarios)
            {
                if (item.Id != id && item.Username == username)
                {
                    uniqueuser = true;
                    break; // Salir del bucle cuando se encuentra una coincidencia
                }
            }

            if (uniqueuser)
            {
                Result res = new Result()
                {
                    Usuario = null,
                    Mensaje = "El Username no puede ser usado, porque otro usuario lo tiene, modifique con otro username"
                };
                return res;
            }
            else {
                Usuario usuario = new Usuario()
                {
                    Id = id,
                    Nombre = nombre,
                    Apellido = apellido,
                    Direccion = direccion,
                    Telefono = telefono,
                    Edad = edad,
                    Correo = correo,
                    Clave = clave,
                    IdRol = idRol,
                    Username = username

                };
                return _usuarioRepository.Edit(usuario);
            }
        }
    }
         
}
    
