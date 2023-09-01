using SistemaVentasSoap.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class ProductoRepository : DbContext , IProductoRepository
    {

        public class Result
        {
            public Producto Producto { get; set; }
            public String Mensaje { get; set; }
        }
        public List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection connection = GetConnection())
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
                                Precio = (decimal)reader["Precio"],
                            UrlImagen = (string)reader["UrlImage"],
                                Descripcion_corta = (string)reader["Descripcion_corta"]
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
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Producto (Descripcion, IdCategoria, stock, precio) VALUES (@Descripcion, @IdCategoria, @stock, @precio)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.Parameters.AddWithValue("@precio", producto.Precio);
                        command.Parameters.AddWithValue("@UrlImagen", producto.UrlImagen);
                        command.Parameters.AddWithValue("@DesCorta", producto.Descripcion_corta);

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
        public Result BuscarProducto(int Id)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection = GetConnection())
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
                            IdCategoria = (int)reader["IdCategoria"],
                            UrlImagen = (string)reader["UrlImage"],
                            Descripcion_corta = (string)reader["Descripcion_corta"]

                            
                        };
                        Categoria categoria = new Categoria()
                        {
                            Id = (int)reader["IdCategoria"],
                            Descripcion = (string)reader["Categoria"]
                        };
                        producto.Categoria = categoria;
                        result.Producto = producto;
                        result.Mensaje = "ok";
                    return result;
                    }
                    else
                    {
                        result.Producto = null;
                        result.Mensaje = "Error no se encuentra el producto";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Producto = null;
                result.Mensaje = ex.ToString();
                return result;
            }
        }
        //metodo para eliminar producto
        public String EliminarProducto(int Id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
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
        public String ActualizarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection connection = GetConnection()){
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Producto SET descripcion = @Descripcion, idCategoria = @IdCategoria, Precio = @Precio, Stock = @Stock, UrlImage=@UrlImagen, Descripciom_corta=@DesCorta WHERE id = @Id; ", connection)) {
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                        command.Parameters.AddWithValue("@stock", producto.Stock);
                        command.Parameters.AddWithValue("@precio", producto.Precio);
                        command.Parameters.AddWithValue("@Id", producto.Id);
                        command.Parameters.AddWithValue("@UrlImagen", producto.UrlImagen);
                        command.Parameters.AddWithValue("@DesCorta", producto.Descripcion_corta);

                        int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)  {
                            return ("Se Actualizo producto");

                        }
                    else { return ("No se encontro el Id del producto");
                        }
                    }
    
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public List<Producto> BuscarProductosDescripcion(string palabra)
        {
           // try {
                List<Producto> listProductos = new List<Producto>();

                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select p.*, c.descripcion as CategoriaProducto  from producto p  inner Join Categoria c on c.id = p.idCategoria where p.Descripcion like '%" + palabra + "%'", connection);
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
                                Precio = (decimal)reader["Precio"],
                                UrlImagen = (string)reader["UrlImage"],
                                Descripcion_corta = (string)reader["Descripcion_corta"]
                            };
                            Categoria categoria = new Categoria
                            {
                                Id = (int)reader["IdCategoria"],
                                Descripcion = (string)reader["CategoriaProducto"]
                            };
                            producto.Categoria = categoria;
                            listProductos.Add(producto);
                        }
                    }
                    return listProductos;
                }
          //  }catch(Exception ex) {
            //    return null;
           // }
        }

    }
}