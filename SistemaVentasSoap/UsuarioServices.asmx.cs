using SistemaVentasSoap.DataAcess;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            return _usuarioRepository.GetAll();
        }


        [WebMethod]
        public String InsertarUsuario(string name, string correo, string clave, int rol)
        {
            Usuario usuario = new Usuario()
            {

                NombresCompleto = name,
                Correo = correo,
                Clave = clave,
                IdRol = rol

            };
            return _usuarioRepository.Create(usuario);
        }

        [WebMethod]
        public Usuario BuscarUsuario(int id)
        {
            return _usuarioRepository.GetById(id);
        }
    }
}
