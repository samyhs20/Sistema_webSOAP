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
        //Metodo para listar categoria
        List<Categoria> GetAll();
        //Metodo para gregar categoria
        string AgregarCategoria(Categoria categoria);
        //Metodo para listar productos por categoria
        List<Producto> ListarPorCategorias(int IdCategoria);
    }
}
