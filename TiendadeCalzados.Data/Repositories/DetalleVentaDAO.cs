using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class DetalleVentaDAO
    {
        private string cadenaConexion = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";

        // INSERTAR DETALLE
        public void Insertar(DetalleVenta detalle)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO DetalleVentas
                      (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                      VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario)", cn);

                cmd.Parameters.AddWithValue("@IdVenta", detalle.IdVenta);
                cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // LISTAR POR VENTA
        public List<DetalleVenta> ListarPorVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                        IdDetalle,
                        IdVenta,
                        IdProducto,
                        Cantidad,
                        PrecioUnitario,
                        (Cantidad * PrecioUnitario) AS SubTotal
                      FROM DetalleVentas
                      WHERE IdVenta = @IdVenta", cn);

                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        IdDetalle = dr.GetInt32(0),
                        IdVenta = dr.GetInt32(1),
                        IdProducto = dr.GetInt32(2),
                        Cantidad = dr.GetInt32(3),
                        PrecioUnitario = dr.GetDecimal(4),
                        SubTotal = dr.GetDecimal(5)
                    });
                }
            }

            return lista;
        }

        // ELIMINAR DETALLE
        public void Eliminar(int idDetalle)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM DetalleVentas WHERE IdDetalle = @IdDetalle", cn);

                cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
