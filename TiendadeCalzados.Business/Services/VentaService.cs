using System.Collections.Generic;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;


namespace TiendadeCalzados.Business.Services
{
    public class VentaService
    {
        private VentaDAO ventaDAO = new VentaDAO();


        public void RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            venta.Total = 0;
            foreach (var d in detalles)
                venta.Total += d.SubTotal;


            int idVenta = ventaDAO.RegistrarVenta(venta);


            foreach (var d in detalles)
            {
                d.IdVenta = idVenta;
                ventaDAO.RegistrarDetalle(d);
            }
        }
        public List<Venta> ListarVentas()
        {
            return ventaDAO.ListarVentas();
        }
    }
}

