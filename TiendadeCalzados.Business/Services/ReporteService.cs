using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class ReporteService
    {
        private ReporteDAO reporteDAO = new ReporteDAO();

        public List<DetalleReporteVenta> ListarReporteDetalleVentas(DateTime inicio, DateTime fin)
        {
            return new ReporteDAO().ObtenerReporteDetalleVentas(inicio, fin);
        }
    }
}
