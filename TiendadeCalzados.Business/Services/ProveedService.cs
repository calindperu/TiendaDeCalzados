using System.Collections.Generic;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;
using System;


namespace TiendadeCalzados.Business.Services
{
    public class ProveedService
    {
        public readonly ProveedDAO proveedDAO;

        // Constructor
        public ProveedService()
        {
            proveedDAO = new ProveedDAO();
        }

        // Listar todos los proveedores
        public List<Proveedor> ListarProveedores()
        {
            return proveedDAO.ListarProveedores();
        }
    }
}
