using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;


namespace TiendaDeCalzados
{
    public partial class FormClientes : Form
    {
        private readonly ClienteService clienteService;

        public FormClientes()
        {
            InitializeComponent();
            clienteService = new ClienteService();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvClientes.AutoGenerateColumns = false;

            dgvClientes.Columns.Add("IdCliente", "Código de Cliente");
            dgvClientes.Columns["IdCliente"].DataPropertyName = "IdCliente";

            dgvClientes.Columns.Add("Nombres", "Nombres");
            dgvClientes.Columns["Nombres"].DataPropertyName = "Nombres";

            dgvClientes.Columns.Add("ApellidoPaterno", "Apellido Paterno");
            dgvClientes.Columns["ApellidoPaterno"].DataPropertyName = "ApellidoPaterno";

            dgvClientes.Columns.Add("ApellidoMaterno", "Apellido Materno");
            dgvClientes.Columns["ApellidoMaterno"].DataPropertyName = "ApellidoMaterno";

            dgvClientes.Columns.Add("Telefono", "Teléfono");
            dgvClientes.Columns["Telefono"].DataPropertyName = "Telefono";

            dgvClientes.Columns.Add("Correo", "Correo");
            dgvClientes.Columns["Correo"].DataPropertyName = "Correo";

            dgvClientes.Columns.Add("Direccion", "Dirección");
            dgvClientes.Columns["Direccion"].DataPropertyName = "Direccion";

            // OBTENER LISTA DE CLIENTES DESDE EL SERVICE
            List<Cliente> clientes = clienteService.ListarClientes();

            dgvClientes.DataSource = clientes;
        }
    }
}
