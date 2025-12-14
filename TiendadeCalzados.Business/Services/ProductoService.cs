using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class ProductoService
    {
        public readonly ProductosDAO productosDAO;

        // Constructor
        public ProductoService()
        {
            productosDAO = new ProductosDAO();
        }

        // Listar todos los proveedores
        public List<Producto> ListarProductos()
        {
            return productosDAO.ListarProductos();
        }
    }
}

