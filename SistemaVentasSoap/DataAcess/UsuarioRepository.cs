using SistemaVentasSoap.DataAcess.Interfaces;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class UsuarioRepository : DbContext , IUsuarioRepository
    {
        //clases para retornar el objeto del repositorio con mensajes y banderas , para controlar errores
        public class Result { 
        public Usuario Usuario { get; set; }
        public string Mensaje { get; set; }    
        }

        public class ResultArray
        {
            public List<Usuario> Usuarios { get; set; }
            public string Mensaje { get; set; }
        }
        public class ResultLogin
        {
            public Usuario Usuario { get; set; }
            public bool Flag { get; set; }
            public string Mensaje { get; set; }
        }

        //metodo para obtener todos los usuarios
        public ResultArray GetAll()
        {
            ResultArray result = new ResultArray();
            List<Usuario> usuarios = new List<Usuario>();
            try {
                using (SqlConnection connection = GetConnection())
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
                                Nombre = (string)reader["Nombre"],
                                Apellido = (string)reader["Apellido"],
                                Direccion = (string)reader["Direccion"],
                                Telefono = (string)reader["Telefono"],
                                Edad = (int)reader["Edad"],
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
                result.Usuarios = usuarios;
                result.Mensaje = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Usuarios = null;
                result.Mensaje = ex.ToString();
                return null;
            }
        }

        //metodo para buscar un usuario en especifico 
        public Result GetById(int id)
        {
            Result result = new Result();
            try
            {
                //conexion de la base de datos
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    //SELECT id, nombresCompleto, correo, dbo.DesencriptarClave(clave) AS clave, idRol
                    //FROM USUARIO;
                    string query = "SELECT " +
                        "u.*, dbo.DesencriptarClave(u.clave) AS ClaveDes," +
                        " r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id WHERE u.Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            Apellido = (string)reader["Apellido"],
                            Direccion = (string)reader["Direccion"],
                            Telefono = (string)reader["Telefono"],
                            Edad = (int)reader["Edad"],
                            Correo = (string)reader["Correo"],
                            IdRol = (int)reader["IdRol"],
                            Username = (string)reader["Username"],
                            Clave = (string)reader["ClaveDes"],
                        };
                        Rol rol = new Rol
                        {
                            Id = (int)reader["IdRol"],
                            Descripcion = (string)reader["Descripcion"]
                        };

                        usuario.Rol = rol;
                        result.Usuario = usuario;
                        result.Mensaje = "OK";
                        return result;
                    }
                    else
                    {
                        result.Usuario = null;
                        result.Mensaje = "No existe el Usuario con el  identificador: "+id;
                        return result; // No se encontró un usuario con ese ID
                    }
                }
            }
            catch (Exception ex)
            {
                result.Usuario = null;
                result.Mensaje = ex.ToString();
                return result;
            }
        }

        //metodo para ingresar un usuario --SIRVE PARA EL REGISTRO DEL USUARIO
        public Result Create(Usuario usuario)
        {
            Result result = new Result();
            bool isUsernameUnique = CheckIfUsernameIsUnique(usuario.Username);
            if (isUsernameUnique)
            {
                try
                {
                    using (SqlConnection connection = GetConnection())
                    {
                        connection.Open();
                        string query = "INSERT INTO Usuario (nombre,username,apellido,direccion,telefono,edad,correo, idRol, clave) VALUES " +
                            "(@Nombre,@Username,@Apellido,@Direccion,@Telefono,@Edad,@Correo, @idRol,dbo.EncriptarClave(@Clave))";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                            command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                            command.Parameters.AddWithValue("@Edad", usuario.Edad);
                            command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                            command.Parameters.AddWithValue("@Correo", usuario.Correo);
                            command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                            command.Parameters.AddWithValue("@Clave", usuario.Clave);
                            command.Parameters.AddWithValue("@Username", usuario.Username);

                            int rowsAffected = command.ExecuteNonQuery();
                            result.Usuario = usuario;
                            result.Mensaje = "Usuario, creado correctamente";
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Usuario = null;
                    result.Mensaje = ex.ToString();
                    return result;
                }
            }
            else
            {
                result.Usuario = null;
                result.Mensaje = "El usuario es único, porfavor ingrese otro nombre de usuario, Username invalid";
                return result;
            }
            
        }
        //metodo para Actualizar un usuario por su ID a un usuario NO hacer un 
        public Result Edit(Usuario usuario)
        {
            Result result = new Result();
            try
            {
                //conexion de la base de datos
                using (SqlConnection connection = GetConnection())
                {
                    //abrimos la conexion
                    connection.Open();
                    //sentencia SQL que se va a ejecutar
                    string query = "UPDATE Usuario SET nombre=@Nombre,username = @Username ,apellido=@Apellido,direccion=@Direccion,telefono=@Telefono,edad=@Edad, idRol=@IdRol, clave = dbo.EncriptarClave(@Clave), Correo = @Correo WHERE Id = @Id";
                    //se realiza dento para ejecutar el query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //se escribe los parametros que se piden siempre que pongas con @es lo que se va a mandar 
                        command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@Username", usuario.Username);
                        command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                        command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                        command.Parameters.AddWithValue("@Edad", usuario.Edad);
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);
                        command.Parameters.AddWithValue("@Id", usuario.Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Usuario = usuario;
                            result.Mensaje = "Actualizacion del Usuario con identificador " + usuario.Id+ " Correcta";
                            return result; // Retorna el usuario editado
                        }
                        else
                        {
                            result.Usuario = null;
                            result.Mensaje = "No se pudo actualizar el usuario compruebe la peticion ";
                            return result; // No se pudo actualizar el usuario
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                result.Usuario = null;
                result.Mensaje = ex.ToString();
                return result;
            }
        }             

        public ResultLogin LoginUser(string username, string pass)
        {
            ResultLogin result = new ResultLogin();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    
                    string query = "SELECT u.* ,dbo.DesencriptarClave(u.clave) AS ClaveDes," +
                        "r.Descripcion AS Descripcion FROM Usuario u INNER JOIN Rol r ON u.IdRol = r.Id " +
                        "where u.username = @Username and dbo.DesencriptarClave(u.clave) = @Clave";

                    SqlCommand command = new SqlCommand(query, connection);
                    //se escribe los parametros que se piden siempre que pongas con @es lo que se va a mandar 
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Clave", pass);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"],
                            Apellido = (string)reader["Apellido"],
                            Direccion = (string)reader["Direccion"],
                            Telefono = (string)reader["Telefono"],
                            Edad = (int)reader["Edad"],
                            Correo = (string)reader["Correo"],
                            IdRol = (int)reader["IdRol"],
                            Username = (string)reader["Username"],
                            Clave = (string)reader["ClaveDes"],
                        };
                        Rol rol = new Rol
                        {
                            Id = (int)reader["IdRol"],
                            Descripcion = (string)reader["Descripcion"]
                        };

                        usuario.Rol = rol;
                        result.Usuario = usuario;
                        result.Flag = true;
                        result.Mensaje = "Ingreso correcto";
                        return result;
                    }
                    else
                    {
                        result.Usuario = null;
                        result.Flag = false;
                        result.Mensaje = "Error en las credenciales Username o Password, intente nuevamente";
                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Usuario = null;
                result.Flag = false;
                result.Mensaje = ex.ToString();
                return result;
            }
           
        }

        //metodo para eliminar usuarios 
        public String Delete(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Usuario where id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Usuario borrado correctamente";
                    }
                    else
                    {
                        return "El usuario no pudo ser encontrado o eliminado";
                    }
                }
            }
            catch(Exception ex)
            { 
                return ex.ToString() + " Error en la ejecucion del codigo, consulte con su proveedor";
            } 
        }

        //Metodos validaciones de Usuario 
        private bool CheckIfUsernameIsUnique(string username)
        {
            using (SqlConnection connection = new DbContext().GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Usuario WHERE username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                return userCount == 0;
            }
        }

    }
}
        
    
