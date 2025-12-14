using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Presentation
{
    public partial class FormDetalleVentas : Form
    {
        private DetalleVentaService detalleService;
        private int idVenta;

        // ================= CONSTRUCTOR PARA DISEÑADOR =================
        public FormDetalleVentas()
        {
            InitializeComponent();
            InicializarServicios();
        }

        // ================= CONSTRUCTOR REAL (RECIBE ID VENTA) =================
        public FormDetalleVentas(int ventaId) : this()
        {
            idVenta = ventaId;
        }

        // ================= INICIALIZACIÓN CENTRAL =================
        private void InicializarServicios()
        {
            detalleService = new DetalleVentaService();
        }

        // ================= EVENTO LOAD =================
        private void FormDetalleVentas_Load(object sender, EventArgs e)
        {
            if (detalleService == null)
            {
                MessageBox.Show("Error interno: Servicio no inicializado");
                return;
            }

            if (idVenta <= 0)
            {
                MessageBox.Show("IdVenta no válido");
                return;
            }
            ConfigurarGridDetalle();
            ListarDetalles();
        }

        // ================= GRID =================

        private void ConfigurarGridDetalle()
        {
            dgvDetalleVentas.AutoGenerateColumns = false;
            dgvDetalleVentas.Columns.Clear();

            dgvDetalleVentas.Columns.Add("IdProducto", "Producto");
            dgvDetalleVentas.Columns["IdProducto"].DataPropertyName = "IdProducto";

            dgvDetalleVentas.Columns.Add("Cantidad", "Cantidad");
            dgvDetalleVentas.Columns["Cantidad"].DataPropertyName = "Cantidad";

            dgvDetalleVentas.Columns.Add("PrecioUnitario", "Precio");
            dgvDetalleVentas.Columns["PrecioUnitario"].DataPropertyName = "PrecioUnitario";

            dgvDetalleVentas.Columns.Add("SubTotal", "Sub Total");
            dgvDetalleVentas.Columns["SubTotal"].DataPropertyName = "SubTotal";
        }



        // ================= BOTÓN AGREGAR =================
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Precio inválido");
                return;
            }

            DetalleVenta detalle = new DetalleVenta
            {
                IdVenta = idVenta,
                IdProducto = Convert.ToInt32(cbProducto.SelectedValue),
                Cantidad = Convert.ToInt32(nudCantidad.Value),
                PrecioUnitario = precio
            };

            detalleService.AgregarDetalle(detalle);
            ListarDetalles();
            LimpiarControles();
        }

        // ================= LISTAR DETALLES =================
        private void ListarDetalles()
        {
            List<DetalleVenta> lista = detalleService.ObtenerDetalles(idVenta);

            dgvDetalleVentas.AutoGenerateColumns = false;
            dgvDetalleVentas.DataSource = null;
            dgvDetalleVentas.DataSource = lista;

            decimal total = lista.Sum(d => d.SubTotal);
            lblTotal.Text = $"TOTAL: S/ {total:0.00}";
        }

        // ================= LIMPIAR =================
        private void LimpiarControles()
        {
            cbProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecio.Clear();
        }
    }
}
