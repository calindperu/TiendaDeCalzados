using System.Collections.Generic;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class DetalleVentaService
    {
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
