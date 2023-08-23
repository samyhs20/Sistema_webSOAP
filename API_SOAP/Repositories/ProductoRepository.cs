using API_SOAP.Data;
using API_SOAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_SOAP.Repositories
{
    public class ProductoRepository
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
                    /*while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            Id = (int)reader["Id"],
                            Descripcion = (string)reader["Descripcion"],
                            IdCategoria = (int)reader["IdCategoria"],
                            Stock = (int)reader["Stock"],
                            Precio = (double)reader["Precio"],
                            Activo = (int)reader["Activo"],
                            FechaRegistro = (DateTime)reader["FechaRegistro"]
                        };

                        productos.Add(producto);
                    }*/
                }
            }

            return productos;
        }

        //implementa los otros metodos para leer, actualizar, eliminar etc.
    }
}