using System;

namespace TiendadeCalzados.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Talla { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }

        // Cambiar de Proveedor a int SI SOLO GUARDAS EL ID
        public int IdProveedor { get; set; }

        public DateTime FechaRegistro { get; set; }


    }
}
