using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class ProductoRepository
    {
        public List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection connection = new DbContext().GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Producto", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            Id = (int)reader["Id"],
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