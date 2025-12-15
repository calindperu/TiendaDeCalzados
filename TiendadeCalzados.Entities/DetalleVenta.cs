namespace TiendadeCalzados.Entities
{
    public class DetalleVenta
    {

        // public int IdDetalleVenta { get; set; }

        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

        // Campos descriptivos (para mostrar en DataGridView)
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Talla { get; set; }

  
}
}
