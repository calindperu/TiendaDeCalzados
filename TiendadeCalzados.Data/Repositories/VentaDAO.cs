using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class VentaDAO
    {
        // ================= REGISTRAR VENTA =================
        public int RegistrarVenta(Venta venta)
        {
            int idVenta;

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"INSERT INTO Ventas (IdCliente, FechaVenta, Total)
                               OUTPUT INSERTED.IdVenta
                               VALUES (@IdCliente, @FechaVenta, @Total)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                cmd.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                cmd.Parameters.AddWithValue("@Total", venta.Total);

                cn.Open();
                idVenta = (int)cmd.ExecuteScalar();
            }
            return idVenta;
        }

        // ================= REGISTRAR DETALLE =================
        public void RegistrarDetalle(DetalleVenta detalle)
        {
            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"INSERT INTO DetalleVentas
                               (IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal)
                               VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @SubTotal)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@IdVenta", detalle.IdVenta);
                cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", detalle.SubTotal);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ================= LISTAR VENTAS =================
        public List<Venta> ListarVentas()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = new SqlConnection("Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT
                    V.IdVenta,
                    V.IdCliente,
                    V.FechaVenta,
                    ISNULL(SUM(DV.Cantidad * DV.PrecioUnitario), 0) AS TotalVenta
                  FROM Ventas V
                  LEFT JOIN DetalleVentas DV ON V.IdVenta = DV.IdVenta
                  GROUP BY V.IdVenta, V.IdCliente, V.FechaVenta", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Venta
                    {
                        IdVenta = dr.GetInt32(0),
                        IdCliente = dr.GetInt32(1),
                        FechaVenta = dr.GetDateTime(2),
                        TotalVenta = dr.GetDecimal(3)
                    });
                }
            }

            return lista;
        }



    }
}
