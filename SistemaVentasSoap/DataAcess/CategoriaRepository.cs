using SistemaVentasSoap.DataAcess.Interfaces;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Windows.Input;

namespace SistemaVentasSoap.DataAcess
{
    public class CategoriaRepository : DbContext, ICategoriaRepository
    {
        public List<Categoria> GetAll()
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select * from CATEGORIA", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria
                            {
                                Id = (int)reader["Id"],
                                Descripcion = (string)reader["Descripcion"]
                            };
                            categorias.Add(categoria);
                        }
                    }
                }
                return categorias;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string AgregarCategoria(Categoria categoria)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Categoria (Descripcion) VALUES (@Descripcion)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                        int rowsAffected = command.ExecuteNonQuery();
                        return "Categoria Creada";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<Producto> ListarPorCategorias(int IdCategoria)
        {
            List<Producto> pcategorias = new List<Producto>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select * from PRODUCTO where idCategoria = @IdCategoria;", connection);
                    command.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto pcategoria = new Producto
                        {
                            Id = (int)reader["Id"],
                            Descripcion = (string)reader["Descripcion"],
                            IdCategoria = (int)reader["IdCategoria"],
                            Stock = (int)reader["Stock"],
                            Precio = (decimal)reader["Precio"],
                             Descripcion_corta = (string)reader["Descripcion_corta"],
                            UrlImagen = (string)reader["UrlImage"]
                        };
                        pcategorias.Add(pcategoria);
                    }
                }

                return pcategorias;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}