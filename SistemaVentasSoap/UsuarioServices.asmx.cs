﻿using SistemaVentasSoap.DataAcess;
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
        public Result InsertarUsuario(string name, string username ,string correo, string clave, int rol)
        {
            Usuario usuario = new Usuario()
            {

                NombresCompleto = name,
                Correo = correo,
                Clave = clave,
                IdRol = rol,
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
    }
    
}
