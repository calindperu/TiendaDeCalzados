using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendadeCalzados.Entities
{
    public class DetalleReporteVenta
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string NombreUsuario { get; set; }
        public string Apellidos { get; set; }
        //public int Total { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Talla { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

      
        public decimal TotalVenta { get; set; }

    }
}
