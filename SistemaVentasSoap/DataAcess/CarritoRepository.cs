using SistemaVentasSoap.DataAcess.Interfaces;
using SistemaVentasSoap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaVentasSoap.DataAcess
{
    public class CarritoRepository : DbContext, ICarrritoRepository
    {
        public class Result
        {
            public DetalleCompra DetalleCompra { get; set; }
            public String Mensaje { get; set; }
        }
        public class ResultCompra
        {
            public Compra Compra { get; set; }
            public bool Flag { get; set; }
            public String Mensaje { get; set; }
        }
        //Metodo para crear la venta 
        public ResultCompra createCompra(Compra compra)
        {
            ResultCompra resultCompra = new ResultCompra();
            String numeroDocumento = "MT-" + Ultimaventa()+1;
            try
            {
                using(SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO COMPRA(numeroDocumento, tipoPago, Total, Idusuario) VALUES (@NumeroDocumento, @TipoPago,@Total, @IdUsuario)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);
                        command.Parameters.AddWithValue("@TipoPago", compra.TipoPago);
                        command.Parameters.AddWithValue("@Total", compra.Total);
                        command.Parameters.AddWithValue("@IdUsuario", compra.IdUsuario);

                        int rowsAffected = command.ExecuteNonQuery();
                        resultCompra.Flag = (rowsAffected>0)? true : false;
                    }
                    string selectQuery = "SELECT * FROM COMPRA WHERE numeroDocumento = @NumeroDocumento";
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Llena los datos de la venta recién creada en resultCompra
                                resultCompra.Compra = new Compra
                                {
                                    Id = (int)reader["Id"],
                                    NumeroDocumento = (string)reader["numeroDocumento"],
                                    TipoPago = (string)reader["tipoPago"],
                                    Total = (decimal)reader["Total"],
                                    IdUsuario = (int)reader["Idusuario"]
                                    // Agrega otras propiedades si es necesario
                                };
                                resultCompra.Mensaje = "Se creo una venta con exito";
                            }
                        }
                    }
                        return resultCompra;
                }
               
            }catch(Exception ex)
            {
                resultCompra.Compra = null;
                resultCompra.Flag = false;
                resultCompra.Mensaje = ex.ToString();
                return null;

            } 
        }
        //Metodo para llenar el detalle con lo que venga del carrito
        public Result CreateCompraDetalleCompra(DetalleCompra detalle, Compra compra)
        {
            Result detalleCompra = new Result();
            detalle.IdVenta = compra.Id;
            try {

                    using (SqlConnection connection = GetConnection())
                    {
                        connection.Open();
                        string query = "INSERT INTO DETALLECOMPRA (idVenta, IdProducto, cantidad, precio) VALUES (@IdVenta , @IdProducto ,@Cantidad,@Precio) ";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdVenta", compra.Id);
                            command.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                            command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            command.Parameters.AddWithValue("@Precio", detalle.Precio);
                            int ArrowAfect = command.ExecuteNonQuery();
                            detalleCompra.DetalleCompra = detalle;
                            detalleCompra.Mensaje = compra.Id + " Detalle Registrado de los productos ";
                            return detalleCompra;
                        }
                    }

            }
            catch (Exception ex)
            {
                detalleCompra.DetalleCompra = null;
                detalleCompra.Mensaje = ex.ToString();
                return detalleCompra;
            }
        }

        private int Ultimaventa()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT MAX(Id) as Id FROM COMPRA";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                   int  result = (reader.Read()) ? (int)reader["Id"] : 0;
                    return result;

                }

            }
            catch (Exception ex)
            {
               return -1;
            }
            
        }
    }
}