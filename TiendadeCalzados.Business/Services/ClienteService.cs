using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class ClienteService
    {
        private readonly ClientesDAO clientesDAO;

        // Constructor
        public ClienteService()
        {
            clientesDAO = new ClientesDAO();
        }

        // Listar todos los clientes
        public List<Cliente> ListarClientes()
        {
            return clientesDAO.ListarClientes();
        }
    }
}
