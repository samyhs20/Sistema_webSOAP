using System;
using System.Web.Services;

namespace API_SOAP
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "hola mundo";
        }

        [WebMethod]
        public string ObtenerTodosLosProductos()
        {
            ProductoService productoService = new ProductoService();
            productoService.GetAll();
            Console.WriteLine("ESTAMOS AQUI");
            return "pruducto";
        }

    }
}
