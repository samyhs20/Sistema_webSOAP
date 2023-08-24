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
                    //SELECT * 
                    string query = "SELECT u.*, r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Id = (int)reader["Id"],
                                NombresCompleto = (string)reader["NombresCompleto"],
                                Correo = (string)reader["Correo"],
                                IdRol = (int)reader["IdRol"],
                                Username = (string)reader["Username"]
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
                    string query = "INSERT INTO Usuario (nombresCompleto, Username,Correo, IdRol, Clave) VALUES (@NombreUsuario, @Username , @Correo, @IdRol, dbo.EncriptarClave(@Clave))";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombresCompleto);
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);
                        command.Parameters.AddWithValue("@Username", usuario.Username);

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
        //metodo para Editar a un usuario NO hacer un 
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
                    string query = "UPDATE Usuario SET NombresCompleto = @Nombre, IdRol = @IdRol, Clave= dbo.EncriptarClave(@Clave), Correo = @Correo WHERE Id = @Id";
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
                    //SELECT id, nombresCompleto, correo, dbo.DesencriptarClave(clave) AS clave, idRol
                    //FROM USUARIO;
                    string query = "SELECT " +
                        "u.Id as Id , u.NombresCompleto as NombresCompleto , u.Correo AS Correo, u.IdRol as Rol, dbo.DesencriptarClave(u.clave) AS Clave," +
                        "r.Id AS IdRol, r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id WHERE u.Id = @Id";
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
                            IdRol = (int)reader["Rol"]
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
                    //reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                return null;
            }
        }

        public bool LoginUser(string username, string pass)
        {
            //try
            //{
                using (SqlConnection connection = new DbContext().GetConnection())
                {
                    connection.Open();
                    string query = "SELECT u.*, r.Descripcion as Descripcion from Usuario u INNER JOIN Rol r ON u.IdRol = r.id where u.username = @Username and dbo.DesencriptarClave(u.clave) = @Clave";

                    SqlCommand command = new SqlCommand(query, connection);
                    //se escribe los parametros que se piden siempre que pongas con @es lo que se va a mandar 
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Clave", pass);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    { 
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            //}
            //catch (Exception ex)
            //{
              //  return false;
            //}
           
        }
    }
}
        
    
