using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasSoap.DataAcess.Interfaces
{
    internal interface ICategoriaRepository
    {
        string AgregarCategoria(Categoria categoria);
        List<Categoria> GetAll();
        List<Producto> ListarPorCategorias(int IdCategoria);




    }
}
