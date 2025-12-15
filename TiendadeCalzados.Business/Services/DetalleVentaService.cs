using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class DetalleVentaService
    {

        private string cadena = "Server=localhost\\SQLEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";

        public void EliminarDetalle(int idDetalle)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM DetalleVenta WHERE IdDetalle = @IdDetalle",cn
                    
                );

                cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);
                cmd.ExecuteNonQuery();
            }
        }



        private DetalleVentaDAO detalleDAO = new DetalleVentaDAO();

        public void AgregarDetalle(DetalleVenta detalle)
        {
            detalleDAO.Insertar(detalle);   // METODO CORRECTO

        }

        public List<DetalleVenta> ObtenerDetalles(int idVenta)
        {
            return detalleDAO.ListarPorVenta(idVenta);
        }
    }
}
