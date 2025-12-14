using System;

namespace TiendadeCalzados.Entities
{
    public class Proveedor
    {

        //public Proveedor IdProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        //public int Estado { get; set; }
        public bool Estado { get; set; }  // si en BD es BIT
        public DateTime FechaIngreso { get; set; }
    }
}
