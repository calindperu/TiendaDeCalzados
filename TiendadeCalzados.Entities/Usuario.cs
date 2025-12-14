using System;

namespace TiendadeCalzados.Entities
{
    public class Usuario
    {
        
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }

        // ID del rol (viene de la tabla Usuarios)
        public int IdRol { get; set; }

        // Objeto Rol (se puede llenar opcionalmente si haces JOIN)
        public Rol Rol { get; set; }

        public int Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

    }
}