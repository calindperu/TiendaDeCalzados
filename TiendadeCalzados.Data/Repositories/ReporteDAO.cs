using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class ReporteDAO
    {
        private string cadena = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";


        public List<DetalleReporteVenta> ObtenerReporteDetalleVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<DetalleReporteVenta> lista = new List<DetalleReporteVenta>();


            using (SqlConnection cn = new SqlConnection(cadena))
            {
                string sql = @"
                                SELECT V.IdVenta, V.FechaVenta,
                                C.NombreUsuario, C.ApellidoPaterno + ' ' + C.ApellidoMaterno AS Apellidos,
                                P.NombreProducto, P.Marca, P.Talla,
                                DV.Cantidad, DV.PrecioUnitario, DV.SubTotal,
                                SUM(DV.SubTotal) OVER(PARTITION BY V.IdVenta) AS TotalVenta
                                FROM Ventas V
                                INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
                                INNER JOIN DetalleVentas DV ON V.IdVenta = DV.IdVenta
                                INNER JOIN Productos P ON DV.IdProducto = P.IdProducto
                                WHERE V.FechaVenta BETWEEN @inicio AND @fin
                                ORDER BY V.FechaVenta, V.IdVenta, P.NombreProducto";


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@inicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fin", fechaFin);


                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    lista.Add(new DetalleReporteVenta
                    {
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        FechaVenta = Convert.ToDateTime(dr["FechaVenta"]),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        NombreProducto = dr["NombreProducto"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Talla = dr["Talla"].ToString(),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                        SubTotal = Convert.ToDecimal(dr["SubTotal"]),
                        TotalVenta = Convert.ToDecimal(dr["TotalVenta"]), // CORREGIDO
                    });
                }
            }
            return lista;
        }
}
}