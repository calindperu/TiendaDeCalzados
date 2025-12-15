using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormProductos : Form
    {
        // CADENA DE CONEXIÓN
        // string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";
        string cad_conexion = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";
        int IdSeleccionado = 0;

        private readonly ProductoService productosService;

        public FormProductos()
        {
            InitializeComponent();
            productosService = new ProductoService();
        }

        private void Listar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "SELECT * FROM Productos";
                SqlDataAdapter da = new SqlDataAdapter(query, cad_conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProductos.DataSource = dt;
            }
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

        private void Guardar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"INSERT INTO Productos
                (NombreProducto, Marca, Talla, PrecioCompra, PrecioVenta, Stock, IdProveedor, FechaIngreso)
                VALUES 
                (@NombreProducto, @Marca, @Talla, @PrecioCompra, @PrecioVenta, @Stock, @IdProveedor, @FechaIngreso)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NombreProducto", txtNombreProducto.Text);
                cmd.Parameters.AddWithValue("@RMarca", txtMarca.Text);
                cmd.Parameters.AddWithValue("@Talla", txtTalla.Text);
                cmd.Parameters.AddWithValue("@PrecioCompra", txtPrecioCompra.Text);
                cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta.Text);
                cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text);
                cmd.Parameters.AddWithValue("@FechaIngreso", DateTime.Now); // Recomendado

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente guardado correctamente");
            }
        }


        private void Modificar()
        {
            if (string.IsNullOrWhiteSpace(txtIdProducto.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de modificar.");
                return;
            }

            if (!int.TryParse(txtIdProducto.Text.Trim(), out int idCliente))
            {
                MessageBox.Show("El Campo IdCliente no es valido, debe ser un numero entero");
                return;
            }

            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"UPDATE Productos SET 
                NombreProducto=@NombreProducto, 
                Marca=@Marca, 
                Talla=@Talla, 
                PrecioCompra=@PrecioCompra, 
                PrecioVenta=@PrecioVenta,                         
                Stock=@Stock,
                IdProveedor=@IdProveedor, 
                FechaRegistro=@FechaRegistro
                WHERE IdProducto=@IdProducto";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdProducto", txtIdProducto.Text);
                cmd.Parameters.AddWithValue("@NombreProducto", txtNombreProducto.Text.Trim());
                cmd.Parameters.AddWithValue("@Marca", txtMarca.Text.Trim());
                cmd.Parameters.AddWithValue("@Talla", txtTalla.Text.Trim());
                cmd.Parameters.AddWithValue("@PrecioCompra", txtPrecioCompra.Text.Trim());
                cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta.Text.Trim());
                cmd.Parameters.AddWithValue("@Stock", txtStock.Text.Trim());
                cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text.Trim());
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);

                cn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente modificado correctamente.");
            }
        }

        private void Eliminar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "DELETE FROM Productos WHERE IdProductos=@IdProductos";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdProductos", txtIdProveedor.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado");
            }
        }


        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtIdProducto.Text = fila.Cells["IdProducto"].Value?.ToString();
                txtNombreProducto.Text = fila.Cells["NombreProducto"].Value?.ToString();
                txtMarca.Text = fila.Cells["Marca"].Value?.ToString();
                txtTalla.Text = fila.Cells["Talla"].Value?.ToString();
                txtPrecioCompra.Text = fila.Cells["PrecioCompra"].Value?.ToString();
                txtPrecioVenta.Text = fila.Cells["PrecioVenta"].Value?.ToString();
                txtStock.Text = fila.Cells["Stock"].Value?.ToString();
                txtIdProveedor.Text = fila.Cells["IdProveedor"].Value?.ToString();


                if (fila.Cells["FechaRegistro"].Value != DBNull.Value)
                {
                    DateTime fecha = Convert.ToDateTime(fila.Cells["FechaRegistro"].Value);
                    txtFechaRegistro.Text = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    txtFechaRegistro.Text = "";
                }
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Guardar();
            Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Modificar();
            Listar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            Listar();
        }
    }
}