using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;


namespace TiendaDeCalzados
{
    public partial class FormClientes : Form
    {
        // CADENA DE CONEXIÓN
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";
         string cad_conexion = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";       
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";
        int IdSeleccionado = 0;

        private readonly ClienteService clienteService;

        public FormClientes()
        {
            InitializeComponent();
            clienteService = new ClienteService();
        }

        private void Listar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "SELECT * FROM Clientes";
                SqlDataAdapter da = new SqlDataAdapter(query, cad_conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvClientes.DataSource = dt;
            }
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvClientes.AutoGenerateColumns = false;

            dgvClientes.Columns.Add("IdCliente", "Código");
            dgvClientes.Columns["IdCliente"].DataPropertyName = "IdCliente";

            dgvClientes.Columns.Add("NombreUsuario", "Nombres");
            dgvClientes.Columns["NombreUsuario"].DataPropertyName = "NombreUsuario";

            dgvClientes.Columns.Add("ApellidoPaterno", "Apellido Paterno");
            dgvClientes.Columns["ApellidoPaterno"].DataPropertyName = "ApellidoPaterno";

            dgvClientes.Columns.Add("ApellidoMaterno", "Apellido Materno");
            dgvClientes.Columns["ApellidoMaterno"].DataPropertyName = "ApellidoMaterno";

            dgvClientes.Columns.Add("Telefono", "Telefono");
            dgvClientes.Columns["Telefono"].DataPropertyName = "Telefono";

            dgvClientes.Columns.Add("Correo", "Correo");
            dgvClientes.Columns["Correo"].DataPropertyName = "Correo";

            dgvClientes.Columns.Add("Direccion", "Dirección");
            dgvClientes.Columns["Direccion"].DataPropertyName = "Direccion";

            dgvClientes.Columns.Add("FechaRegistro", "Fecha de registro");
            dgvClientes.Columns["FechaRegistro"].DataPropertyName = "FechaRegistro";

            List<Cliente> clientes = clienteService.ListarClientes();
            dgvClientes.DataSource = clientes;
        }

        private void Guardar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"INSERT INTO Clientes 
                (NombreUsuario, ApellidoPaterno, ApellidoMaterno, Telefono, Correo, Direccion, FechaRegistro)
                VALUES (@Nombres, @ApellidoPaterno, @ApellidoMaterno, @Telefono, @Correo, @Direccion, @FechaRegistro)";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);
                cmd.Parameters.AddWithValue("@NombreUsuario", txtNombreUsuario.Text);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txtPaterno.Text);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txtMaterno.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now); // Recomendado

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente guardado correctamente");
            }
        }

        private void Modificar()
        {
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente antes de modificar.");
                return;
            }

            if (!int.TryParse(txtIdCliente.Text.Trim(), out int idCliente))
            {
                MessageBox.Show("El Campo IdCliente no es valido, debe ser un numero entero");
                return;
            }

            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"UPDATE Clientes SET 
                NombreUsuario=@NombreUsuario, 
                ApellidoPaterno=@ApellidoPaterno, 
                ApellidoMaterno=@ApellidoMaterno, 
                Telefono=@Telefono, 
                Correo=@Correo,                         
                Direccion=@Direccion, 
                FechaRegistro=@FechaRegistro
                WHERE IdCliente=@IdCliente";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@NombreUsuario", txtNombreUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txtPaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txtMaterno.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
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
                string query = "DELETE FROM Clientes WHERE IdCliente=@IdCliente";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado");
            }
        }

          

        private void bntActualizar_Click(object sender, EventArgs e)
        {
            Modificar();
            Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Guardar();
            Listar();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            Listar();
        }


        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                txtIdCliente.Text = fila.Cells["IdCliente"].Value?.ToString();
                txtNombreUsuario.Text = fila.Cells["NombreUsuario"].Value?.ToString();
                txtPaterno.Text = fila.Cells["ApellidoPaterno"].Value?.ToString();
                txtMaterno.Text = fila.Cells["ApellidoMaterno"].Value?.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();

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



    }
}
