using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;
using TiendadeCalzados.Presentation;   // 👈 IMPORTANTE

namespace TiendaDeCalzados
{
    public partial class FormVentas : Form
    {
        private VentaService ventaService = new VentaService();

        public FormVentas()
        {
            InitializeComponent();
        }

        // ========================= LOAD =========================
        private void FormVentas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarVentas();           

        }

        // ========================= CONFIGURAR GRID =========================
        private void ConfigurarGrid()
        {
            dgvVentas.AutoGenerateColumns = false;
            dgvVentas.Columns.Clear();

            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.MultiSelect = false;
            dgvVentas.ReadOnly = true;

            dgvVentas.Columns.Add("IdVenta", "Código Venta");
            dgvVentas.Columns["IdVenta"].DataPropertyName = "IdVenta";

            dgvVentas.Columns.Add("IdCliente", "Código Cliente");
            dgvVentas.Columns["IdCliente"].DataPropertyName = "IdCliente";

            dgvVentas.Columns.Add("FechaVenta", "Fecha de Venta");
            dgvVentas.Columns["FechaVenta"].DataPropertyName = "FechaVenta";

            dgvVentas.Columns.Add("TotalVenta", "Total Venta");
            dgvVentas.Columns["TotalVenta"].DataPropertyName = "TotalVenta";
        
        }

          

        // ========================= CARGAR VENTAS =========================
        private void CargarVentas()
        {
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = ventaService.ListarVentas();
            
        }

        // ========================= VER DETALLE =========================
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show("No hay ventas registradas");
                return;
            }

            if (dgvVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una venta");
                return;
            }

            int idVenta = Convert.ToInt32(
                dgvVentas.SelectedRows[0].Cells["IdVenta"].Value
            );

            FormDetalleVentas frm = new FormDetalleVentas(idVenta);
            frm.ShowDialog();
        }
    }
}

