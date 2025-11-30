using System;

namespace TiendadeCalzados.Entities
{
    public class Producto
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

    }
}
