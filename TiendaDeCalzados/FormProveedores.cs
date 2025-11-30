using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormProveedores : Form
    {
        private readonly ProveedService proveedService;

        public FormProveedores()
        {
            InitializeComponent();
            proveedService = new ProveedService();
        }

        private void FormProveedores_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.Columns.Clear();

            dgvProveedores.Columns.Add("IdProveedor", "Código");
            dgvProveedores.Columns["IdProveedor"].DataPropertyName = "IdProveedor";

            dgvProveedores.Columns.Add("RazonSocial", "Razón Social");
            dgvProveedores.Columns["RazonSocial"].DataPropertyName = "RazonSocial";

            dgvProveedores.Columns.Add("RUC", "RUC");
            dgvProveedores.Columns["RUC"].DataPropertyName = "RUC";

            dgvProveedores.Columns.Add("Telefono", "Teléfono");
            dgvProveedores.Columns["Telefono"].DataPropertyName = "Telefono";

            dgvProveedores.Columns.Add("Correo", "Correo");
            dgvProveedores.Columns["Correo"].DataPropertyName = "Correo";

            dgvProveedores.Columns.Add("Direccion", "Dirección");
            dgvProveedores.Columns["Direccion"].DataPropertyName = "Direccion";

            dgvProveedores.Columns.Add("FechaIngreso", "Fecha de Ingreso");
            dgvProveedores.Columns["FechaIngreso"].DataPropertyName = "FechaIngreso";

            // OBTENER LISTA DESDE EL SERVICE
            List<Proveedor> listaProveedores = proveedService.ListarProveedores();

            dgvProveedores.DataSource = listaProveedores;
        }
    }
}
