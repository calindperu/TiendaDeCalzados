using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Presentation
{
    public partial class FormDetalleVentas : Form
    {
        private DetalleVentaService detalleService;
        private ProductoService productoService;

        private int idVenta;
        private Producto productoSeleccionado;

        // ================= CONSTRUCTOR DISEÑADOR =================
        public FormDetalleVentas()
        {
            InitializeComponent();
            InicializarServicios();
        }

        // ================= CONSTRUCTOR REAL =================
        public FormDetalleVentas(int ventaId) : this()
        {
            idVenta = ventaId;
        }

        // ================= INICIALIZAR SERVICES =================
        private void InicializarServicios()
        {
            detalleService = new DetalleVentaService();
            productoService = new ProductoService();
        }

        // ================= LOAD =================
        private void FormDetalleVentas_Load(object sender, EventArgs e)
        {
            if (idVenta <= 0)
            {
                MessageBox.Show("IdVenta no válido");
                Close();
                return;
            }

            txtPrecio.ReadOnly = true;
            ConfigurarGridDetalle();
            CargarProductos();
            InicializarCantidad();
            ListarDetalles();
        }

        // ================= GRID =================
        private void ConfigurarGridDetalle()
        {
            dgvDetalleVentas.AutoGenerateColumns = false;
            dgvDetalleVentas.Columns.Clear();

            dgvDetalleVentas.Columns.Add("IdDetalle", "IdDetalle");
            dgvDetalleVentas.Columns["IdDetalle"].DataPropertyName = "IdDetalle";
            dgvDetalleVentas.Columns["IdDetalle"].Visible = false;

            dgvDetalleVentas.Columns.Add("IdProducto", "Producto");
            dgvDetalleVentas.Columns["IdProducto"].DataPropertyName = "IdProducto";

            dgvDetalleVentas.Columns.Add("Cantidad", "Cantidad");
            dgvDetalleVentas.Columns["Cantidad"].DataPropertyName = "Cantidad";

            dgvDetalleVentas.Columns.Add("PrecioUnitario", "Precio");
            dgvDetalleVentas.Columns["PrecioUnitario"].DataPropertyName = "PrecioUnitario";

            dgvDetalleVentas.Columns.Add("SubTotal", "SubTotal");
            dgvDetalleVentas.Columns["SubTotal"].DataPropertyName = "SubTotal";
            
        }

        // ================= CARGAR PRODUCTOS =================
        private void CargarProductos()
        {
            cbProducto.DataSource = productoService.ListarProductos();
            cbProducto.DisplayMember = "NombreProducto";
            cbProducto.ValueMember = "IdProducto";
            cbProducto.SelectedIndex = -1;

        }

      
        // ================= INICIALIZAR CANTIDAD =================
        private void InicializarCantidad()
        {
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 100;
            nudCantidad.Value = 1;
        }

        // ================= COMBO PRODUCTO =================
        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evita ejecuciones cuando el ComboBox aún se está cargando
            if (cbProducto.SelectedIndex == -1)
            {
                txtPrecio.Clear();
                return;
            }

            // Obtiene el producto seleccionado
            Producto prod = cbProducto.SelectedItem as Producto;
            if (prod == null) return;

            // Guarda el producto actual
            productoSeleccionado = prod;

            // Muestra el precio
            txtPrecio.Text = prod.PrecioVenta.ToString("0.00");

            // Configura la cantidad según stock
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = prod.Stock > 0 ? prod.Stock : 1;
            nudCantidad.Value = 1;
        }


        // ================= AGREGAR DETALLE =================
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }

            if (nudCantidad.Value > productoSeleccionado.Stock)
            {
                MessageBox.Show("Cantidad supera el stock disponible");
                return;
            }

            DetalleVenta detalle = new DetalleVenta
            {
                IdVenta = idVenta,
                IdProducto = productoSeleccionado.IdProducto,
                Cantidad = (int)nudCantidad.Value,
                PrecioUnitario = productoSeleccionado.PrecioVenta
            };

            detalleService.AgregarDetalle(detalle);

            ListarDetalles();
            LimpiarControles();
        }

        // ================= LISTAR DETALLES =================
        private void ListarDetalles()
        {
            List<DetalleVenta> lista = detalleService.ObtenerDetalles(idVenta);

            dgvDetalleVentas.DataSource = null;
            dgvDetalleVentas.DataSource = lista;

            decimal total = lista.Sum(d => d.SubTotal);
            lblTotal.Text = $"TOTAL: S/ {total:0.00}";
        }

        //================= ELIMINAR DETALLE (DOBLE CLIC) =================
        /* private void dgvDetalleVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex < 0) return;

             int idDetalle = Convert.ToInt32(
                 dgvDetalleVentas.Rows[e.RowIndex].Cells["IdDetalle"].Value
             );

             DialogResult r = MessageBox.Show(
                 "¿Desea eliminar este producto del detalle?",
                 "Confirmar",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question
             );

             if (r == DialogResult.Yes)
             {
                 detalleService.EliminarDetalle(idDetalle);
                 ListarDetalles();
             }
         }
        */

        public void EliminarDetalle(int idDetalle)
        {
            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True"))           

            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM DetalleVenta WHERE IdDetalle = @IdDetalle", cn))
                {
                    cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        private void dgvDetalleVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idDetalle = Convert.ToInt32(
                dgvDetalleVentas.Rows[e.RowIndex].Cells["IdDetalle"].Value
            );

            DialogResult r = MessageBox.Show(
                "¿Desea eliminar este producto del detalle?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r == DialogResult.Yes)
            {
                detalleService.EliminarDetalle(idDetalle);
                ListarDetalles();
            }
        }



        // ================= LIMPIAR =================
        private void LimpiarControles()
        {
            cbProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecio.Clear();
            productoSeleccionado = null;
        }

        private void dgvDetalleVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
