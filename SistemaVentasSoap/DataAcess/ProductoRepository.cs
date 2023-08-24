using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class ProductoRepository : IProductoRepository
    {
        public List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT P.*, C.descripcion AS CategoriaProducto FROM Producto P INNER JOIN Categoria C ON P.IdCategoria = C.Id;", connection);
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
                            Categoria categoria = new Categoria
                            {
                                Id = (int)reader["IdCategoria"],
                                Descripcion = (string)reader["CategoriaProducto"]
                            };
                            producto.Categoria = categoria;
                            productos.Add(producto);
                        }
                    }

                }
                return productos;
            }
            catch (Exception ex) 
            { 
                return null;
            }
        }
        //metodo para ingresar productos
        public String Create(Producto producto)
        {
            try
            {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Producto (Descripcion, IdCategoria, stock, precio) VALUES (@Descripcion, @IdCategoria, @stock, @precio)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.Parameters.AddWithValue("@precio", producto.Precio);

                        int rowsAffected = command.ExecuteNonQuery();
                        return "Producto Creado";
                    }
                    //return "";

                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //metodo para buscar un producto 
        public Producto BuscarProducto(int Id)
        {
            try
            {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT P.*, C.descripcion As Categoria FROM Producto P INNER JOIN Categoria C ON P.IdCategoria = C.Id Where P.Id = @BuscarProducto;", connection);
                    command.Parameters.AddWithValue("@BuscarProducto", Id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) 
                    {
                        Producto producto = new Producto()
                        {
                            Id = (int)reader["Id"],
                            Descripcion = (string)reader["Descripcion"],
                            Stock = (int)reader["Stock"],
                            Precio = (decimal)reader["Precio"],
                            IdCategoria = (int)reader["IdCategoria"]
                        };
                        Categoria categoria = new Categoria()
                        {
                            Id = (int)reader["IdCategoria"],
                            Descripcion = (string)reader["Categoria"]
                        };
                        producto.Categoria = categoria;
                    return producto;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //metodo para eliminar producto
        public String EliminarProducto(int Id)
        {
            try
            {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Producto Where Id = @IdProductoEliminar;", connection);
                    command.Parameters.AddWithValue("@IdProductoEliminar", Id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        return "Producto Eliminado";
                    }
                    else
                    {
                        return "No se encontro el producto a eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}