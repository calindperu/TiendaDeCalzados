using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Presentation
{
    public partial class FormReportes : Form
    {
        private ReporteService reporteService;

        public FormReportes()
        {
            InitializeComponent();
            reporteService = new ReporteService();
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvReporteVentas.AutoGenerateColumns = false;
            dgvReporteVentas.Columns.Clear();

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "IdVenta",
                HeaderText = "ID Venta",
                Width = 50
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "FechaVenta",
                HeaderText = "Fecha",
                Width = 100
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NombreUsuario",
                HeaderText = "Nombres",
                Width = 120
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Apellidos",
                HeaderText = "Apellidos",
                Width = 120
            });

            // AGREGADOS
            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NombreProducto",
                HeaderText = "Nombre Producto",
                Width = 200
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Marca",
                HeaderText = "Marca Producto",
                Width = 80
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Talla",
                HeaderText = "Talla Producto",
                Width = 50
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad Producto",
                Width = 60
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Producto",
                Width = 80
            });

            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SubTotal",
                HeaderText = "SubTotal Producto",
                Width = 80
            });


            dgvReporteVentas.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TotalVenta",
                HeaderText = "Total Venta",
                Width = 80,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            if (dtpFechaInicio.Value.Date > dtpFechaFin.Value.Date)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            List<DetalleReporteVenta> listaDetalle = reporteService.ListarReporteDetalleVentas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);

            dgvReporteVentas.AutoGenerateColumns = true;
            dgvReporteVentas.DataSource = listaDetalle;

            // Calcular total general por venta (evitando duplicados por detalle)
            decimal totalGeneral = listaDetalle
                .GroupBy(d => d.IdVenta)
                .Select(g => g.First().TotalVenta)
                .Sum();

            lblTotalVentas.Text = "TOTAL VENTAS: S/ " + totalGeneral.ToString("0.00");
        }
    }
}

