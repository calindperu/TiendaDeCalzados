using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace TiendaDeCalzados
{
    public partial class Form1 : Form
    {
        /* https://www.youtube.com/watch?v=ZyiaKtzHKOI */

        /*
        string connectionString = "Server=localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True;";
        string connectionString = "Server=localhost\\SLQEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;"; 
        string connectionString = "Server=PC_GTIC09\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";
        Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True
        "Server=localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True;"

        */

        string connectionString = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";
        int IdSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void CargarDatos()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios";
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))

            {
                //string query = "INSERT INTO Usuarios (Usuario, Clave, NombreCompleto, Correo, IdRol, Estado, FechaRegistro) VALUES (@Usuario, @Clave, @NombreCompleto, @Correo, @IdRol, @Estado, @FechaRegistro)";
                string query =  "INSERT INTO Usuarios (Usuario, Clave, NombreCompleto, Correo, IdRol, Estado, FechaRegistro) " +
                                "VALUES (@Usuario, @Clave, @NombreCompleto, @Correo, @IdRol, @Estado, @FechaRegistro)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@Clave", txtClave.Text);
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@IdRol", int.Parse(txtIdRol.Text));

                // Estado como bool → BIT
                //bool estado = txtEstado.Text == "1" || txtEstado.Text.ToLower() == "true"; 
                //cmd.Parameters.AddWithValue("@Estado", estado);

                // A / I → BIT
                bool estado = (txtEstado.Text == "1" || txtEstado.Text.ToLower() == "true");
                cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;

                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            CargarDatos();
            LimpiarCampos();
            MessageBox.Show("Registro AGREGADO correctamente");
        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtClave.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtIdRol.Clear();
            txtEstado.Clear();
            IdSeleccionado = 0;
        }


       private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (IdSeleccionado == 0) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios 
                             SET Usuario=@Usuario, 
                             Clave=@Clave, 
                             NombreCompleto=@NombreCompleto, 
                             Correo=@Correo, 
                             IdRol=@IdRol, 
                             Estado=@Estado                         
                             WHERE IdUsuario=@IdUsuario";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@Clave", txtClave.Text.Trim());
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                //cmd.Parameters.AddWithValue("@IdRol", txtIdRol.Text.Trim());
                //cmd.Parameters.AddWithValue("@Estado", txtEstado.Text.Trim());         

               
                int idRol;
                if (!int.TryParse(txtIdRol.Text, out idRol))
                {
                    MessageBox.Show("IdRol debe ser un número entero.");
                    return;
                }
                cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = idRol;
                
                bool estado = (txtEstado.Text == "1" || txtEstado.Text.ToLower() == "true");
                cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;

                cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdSeleccionado;

                conn.Open();
                int filas = cmd.ExecuteNonQuery();

                if (filas == 0)
                {
                    MessageBox.Show("No se actualizó ningún registro. Verifique el IdUsuario.");
                    return;
                }
            }

            CargarDatos();
            LimpiarCampos();
            MessageBox.Show("Registro ACTUALIZADO correctamente");
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (IdSeleccionado == 0) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Usuarios WHERE IdUsuario=@IdUsuario";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", IdSeleccionado);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            CargarDatos();
            LimpiarCampos();
            MessageBox.Show("Registro ELIMINADO correctamente");
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];
                IdSeleccionado = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                txtUsuario.Text = fila.Cells["NombreUsuario"].Value.ToString();
                txtClave.Text = fila.Cells["Clave"].Value.ToString();
                txtNombre.Text = fila.Cells["NombreCompleto"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtIdRol.Text = fila.Cells["IdRol"].Value.ToString();
                txtEstado.Text = fila.Cells["Estado"].Value.ToString();
                
                //txtFechaRegistro.Text = fila.Cells["FechaRegistro"].Value.ToString();
            }
        }
    }
}










