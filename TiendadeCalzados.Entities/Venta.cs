using System;

namespace TiendadeCalzados.Entities
{
    public class Venta
    {

        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }

        public decimal TotalVenta { get; set; }

    }
}
