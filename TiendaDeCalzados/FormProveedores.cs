using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormProveedores : Form
    {
        // CADENA DE CONEXIÓN
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";
        string cad_conexion = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";

        int IdSeleccionado = 0;

        private readonly ProveedService proveedService;

        public FormProveedores()
        {
            InitializeComponent();
            proveedService = new ProveedService();
        }

        private void Listar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "SELECT * FROM Proveedores";
                SqlDataAdapter da = new SqlDataAdapter(query, cad_conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvProveedores.DataSource = dt;
            }
        }


        private void btnListar_Click(object sender, EventArgs e)
        {
            Listar();
        }


        private void FormProveedores_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.Columns.Clear();

            dgvProveedores.Columns.Add("IdProveedor", "Código");
            dgvProveedores.Columns["IdProveedor"].DataPropertyName = "IdProveedor";

            dgvProveedores.Columns.Add("RUC", "RUC");
            dgvProveedores.Columns["RUC"].DataPropertyName = "RUC";

            dgvProveedores.Columns.Add("RazonSocial", "Razón Social");
            dgvProveedores.Columns["RazonSocial"].DataPropertyName = "RazonSocial";

            dgvProveedores.Columns.Add("Direccion", "Dirección");
            dgvProveedores.Columns["Direccion"].DataPropertyName = "Direccion";

            dgvProveedores.Columns.Add("Telefono", "Teléfono");
            dgvProveedores.Columns["Telefono"].DataPropertyName = "Telefono";

            dgvProveedores.Columns.Add("Correo", "Correo");
            dgvProveedores.Columns["Correo"].DataPropertyName = "Correo";

            dgvProveedores.Columns.Add("Estado", "Estado");
            dgvProveedores.Columns["Estado"].DataPropertyName = "Estado";

            dgvProveedores.Columns.Add("FechaIngreso", "Fecha de Ingreso");
            dgvProveedores.Columns["FechaIngreso"].DataPropertyName = "FechaIngreso";

            // OBTENER LISTA DESDE EL SERVICE
            List<Proveedor> listaProveedores = proveedService.ListarProveedores();

            dgvProveedores.DataSource = listaProveedores;
        }

        private void Guardar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"INSERT INTO Proveedores
                (RUC, RazonSocial, Direccion, Telefono, Correo, Estado, FechaIngreso)
                VALUES 
                (@RUC, @RazonSocial, @Direccion, @Telefono, @Correo, @Estado, @FechaIngreso)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text);
                cmd.Parameters.AddWithValue("@RUC", txtRUC.Text);
                cmd.Parameters.AddWithValue("@RazonSocial", txtRazonSocial.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text);
                cmd.Parameters.AddWithValue("@FechaIngreso", DateTime.Now); // Recomendado

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente guardado correctamente");
            }
        }

        private void Modificar()
        {
            if (string.IsNullOrWhiteSpace(txtIdProveedor.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de modificar.");
                return;
            }

            if (!int.TryParse(txtIdProveedor.Text.Trim(), out int idCliente))
            {
                MessageBox.Show("El Campo IdCliente no es valido, debe ser un numero entero");
                return;
            }

            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"UPDATE Proveedores SET 
                RUC=@RUC, 
                RazonSocial=@RazonSocial, 
                Direccion=@Direccion, 
                Telefono=@Telefono, 
                Correo=@Correo,                         
                Estado=@Estado, 
                FechaIngreso=@FechaIngreso
                WHERE IdProveedor=@IdProveedor";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text);
                cmd.Parameters.AddWithValue("@RUC", txtRUC.Text.Trim());
                cmd.Parameters.AddWithValue("@RazonSocial", txtRazonSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                cmd.Parameters.AddWithValue("@Estado", txtEstado.Text.Trim());
                cmd.Parameters.AddWithValue("@FechaIngreso", DateTime.Now);

                cn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente modificado correctamente.");
            }
        }

        private void Eliminar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "DELETE FROM Proveedores WHERE IdProveedor=@IdProveedor";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado");
            }
        }


        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtIdProveedor.Text = fila.Cells["IdProveedor"].Value?.ToString();
                txtRUC.Text = fila.Cells["RUC"].Value?.ToString();
                txtRazonSocial.Text = fila.Cells["RazonSocial"].Value?.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();
                txtEstado.Text = fila.Cells["Estado"].Value?.ToString();

                if (fila.Cells["FechaIngreso"].Value != DBNull.Value)
                {
                    DateTime fecha = Convert.ToDateTime(fila.Cells["FechaIngreso"].Value);
                    txtFechaIngreso.Text = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    txtFechaIngreso.Text = "";
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            Modificar();
            Listar();
        }

        private void btnListar_Click_1(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            Listar();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            Guardar();
            Listar();
        }
    }
}
