using System;


namespace TiendadeCalzados.Entities
{
    public class DetalleVenta
    {
        public int InDetalle { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public Decimal SubTotal { get; set; }

        /*
        public int InDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public Decimal SubTotal { get; set; }
        */
    }
}
