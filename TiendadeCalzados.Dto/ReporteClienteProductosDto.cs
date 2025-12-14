namespace TiendadeCalzados.Dto
{
    public class ReporteClienteProductosDto
    {
        public int IdCliente { get; set; }
        public string NombreUsuario { get; set; }
        public string Apellidos { get; set; }
        public string ProductosComprados { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public string Talla { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

    }
}
