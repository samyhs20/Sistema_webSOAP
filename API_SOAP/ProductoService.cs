using API_SOAP.Contracts;
using API_SOAP.Models;
using API_SOAP.Repositories;
using API_SOAP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_SOAP
{
    public class ProductoService : IProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService()
        {
            _productoRepository = new ProductoRepository();
        }

        public List<Producto> GetAll()
        {
            return _productoRepository.GetAll();
            //implementa los otros metodos para leer, acutalizario, eliminar usuario
        }
    }
}