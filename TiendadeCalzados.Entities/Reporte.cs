using System;


namespace TiendadeCalzados.Entities
{
    public class Reporte
    {
        public int IdReporte { get; set; }
        public string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string Detalle { get; set; }
        public Usuario Usuario { get; set; }

        // public int IdUsuario { get; set; }

        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public decimal TotalVenta { get; set; }

    }
}
