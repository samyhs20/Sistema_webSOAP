using SistemaVentasSoap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.Services
{
    public class ProductoService: IProductoService
    { 
        public List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM PRODUCTO", connection);
                Console.WriteLine("conexion establecida");
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    Console.WriteLine("estamos aqui "+ reader.Read());
                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            Descripcion = (string)reader["Descripcion"],
                            IdCategoria = (int)reader["IdCategoria"],
                            Stock = (int)reader["Stock"],
                            Precio = (decimal)reader["Precio"]
                        };
                        productos.Add(producto);
                    }
                }
            }

            return productos;
        }
    }
}