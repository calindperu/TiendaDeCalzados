using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormProductos : Form
    {
        private readonly ProductoService productosService;

        public FormProductos()
        {
            InitializeComponent();
            productosService = new ProductoService();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            dgvProductos.Columns.Add("IdProducto", "Código");
            dgvProductos.Columns["IdProducto"].DataPropertyName = "IdProducto";

            dgvProductos.Columns.Add("NombreProducto", "Nombre");
            dgvProductos.Columns["NombreProducto"].DataPropertyName = "NombreProducto";

            dgvProductos.Columns.Add("Marca", "Marca");
            dgvProductos.Columns["Marca"].DataPropertyName = "Marca";

            dgvProductos.Columns.Add("Talla", "Talla");
            dgvProductos.Columns["Talla"].DataPropertyName = "Talla";

            dgvProductos.Columns.Add("PrecioCompra", "Precio Compra");
            dgvProductos.Columns["PrecioCompra"].DataPropertyName = "PrecioCompra";

            dgvProductos.Columns.Add("PrecioVenta", "Precio Venta");
            dgvProductos.Columns["PrecioVenta"].DataPropertyName = "PrecioVenta";

            dgvProductos.Columns.Add("Stock", "Stock");
            dgvProductos.Columns["Stock"].DataPropertyName = "Stock";

            dgvProductos.Columns.Add("IdProveedor", "Proveedor");
            dgvProductos.Columns["IdProveedor"].DataPropertyName = "IdProveedor";

            dgvProductos.Columns.Add("FechaRegistro", "Fecha Registro");
            dgvProductos.Columns["FechaRegistro"].DataPropertyName = "FechaRegistro";

            // OBTENER LISTA DESDE EL SERVICE
            List<Producto> listaProductos = productosService.ListarProductos();

            dgvProductos.DataSource = listaProductos;
        }
    }
}