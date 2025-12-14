using System.Collections.Generic;
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
