using SistemaVentasSoap.DataAcess.Interfaces;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //metodo para obtener todos los usuarios
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    //p INNER JOIN Categorias c ON p.IdCategoria = c.Id WHERE p.Id = @ProductoId
                    SqlCommand command = new SqlCommand("SELECT u.*, r.Id AS IdRol, r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Id = (int)reader["Id"],
                                NombresCompleto = (string)reader["NombresCompleto"],
                                Correo = (string)reader["Correo"],
                                IdRol = (int)reader["IdRol"]
                            };

                            Rol rol = new Rol
                            {
                                Id = (int)reader["IdRol"],
                                Descripcion = (string)reader["Descripcion"]
                            };

                            usuario.Rol = rol;
                            usuarios.Add(usuario);
                        }
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //metodo para ingresar un usuario
        public String Create(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Usuario (nombresCompleto, Correo, IdRol, Clave) VALUES (@NombreUsuario, @Correo, @IdRol, @Clave)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombresCompleto);
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);

                        int rowsAffected = command.ExecuteNonQuery();
                        return "Creado usuario";
                    }
                    //return "Si pa aqui";

                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //metodo para Editar a un usuario
        public  Usuario Edit(Usuario usuario)
        {
            try
            {
                //conexion de la base de datos
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    //abrimos la conexion
                    connection.Open();
                    //sentencia SQL que se va a ejecutar
                    string query = "UPDATE Usuario SET NombresCompleto = @Nombre, IdRol = @IdRol, Clave=@Clave, Correo = @Correo WHERE Id = @Id";
                    //se realiza dento para ejecutar el query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //se escribe los parametros que se piden siempre que pongas con @es lo que se va a mandar 
                        command.Parameters.AddWithValue("@NombresCompleto", usuario.NombresCompleto);
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return usuario; // Retorna el usuario editado
                        }
                        else
                        {
                            return null; // No se pudo actualizar el usuario
                        }
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //metodo para buscar un usuario en especifico 
        public Usuario GetById(int id)
        {
            try
            {
                //conexion de la base de datos
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    string query = "SELECT u.*, r.Id AS IdRol, r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id WHERE u.Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            NombresCompleto = (string)reader["NombresCompleto"],
                            Correo = (string)reader["Correo"],
                            Clave = (string)reader["Clave"],
                            IdRol = (int)reader["IdRol"]
                        };
                        Rol rol = new Rol
                        {
                            Id = (int)reader["IdRol"],
                            Descripcion = (string)reader["Descripcion"]
                        };

                        usuario.Rol = rol;
                        return usuario;
                    }
                    else
                    {
                        return null; // No se encontró un usuario con ese ID
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                return null;
            }
        }
    }
}
        
    
