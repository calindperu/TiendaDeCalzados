using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;


namespace TiendaDeCalzados
{
    public partial class FormUsuarios : Form
    {
        // CADENA DE CONEXIÓN
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";
        string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";
        //string cad_conexion = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";
        int IdSeleccionado = 0;

        private readonly UsuarioService usuarioService;

        public FormUsuarios()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
        }

        // CORREGIDO: EL PARÁMETRO Y LA VARIABLE TENÍAN EL MISMO NOMBRE
        private void Listar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "SELECT * FROM Usuarios";
                SqlDataAdapter da = new SqlDataAdapter(query, cad_conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            dgvUsuarios.AutoGenerateColumns = false;

            dgvUsuarios.Columns.Add("IdUsuario", "Código usuario");
            dgvUsuarios.Columns["IdUsuario"].DataPropertyName = "IdUsuario";

            dgvUsuarios.Columns.Add("NombreUsuario", "Nombre Usuario");
            dgvUsuarios.Columns["NombreUsuario"].DataPropertyName = "NombreUsuario";

            dgvUsuarios.Columns.Add("Clave", "Clave de Usuario");
            dgvUsuarios.Columns["Clave"].DataPropertyName = "Clave";

            dgvUsuarios.Columns.Add("NombreCompleto", "Nombres de Usuario");
            dgvUsuarios.Columns["NombreCompleto"].DataPropertyName = "NombreCompleto";

            dgvUsuarios.Columns.Add("Correo", "Email de Usuario");
            dgvUsuarios.Columns["Correo"].DataPropertyName = "Correo";

            dgvUsuarios.Columns.Add("IdRol", "Rol de usuario");
            dgvUsuarios.Columns["IdRol"].DataPropertyName = "IdRol";

            dgvUsuarios.Columns.Add("Estado", "Estado");
            dgvUsuarios.Columns["Estado"].DataPropertyName = "Estado";

            dgvUsuarios.Columns.Add("FechaRegistro", "Fecha de registro");
            dgvUsuarios.Columns["FechaRegistro"].DataPropertyName = "FechaRegistro";

            // CORREGIDO: VALIDAR LA SESIÓN PARA EVITAR NULLREFERENCEEXCEPTION
            Usuario usuarioActual = SesionActual.UsuarioLogueado;

            if (usuarioActual == null)
            {
                MessageBox.Show("No hay un usuario logueado en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SI TU MÉTODO REQUIERE EL ID DEL USUARIO LOGUEADO
            List<Usuario> usuarios = usuarioService.ListarUsuarios(usuarioActual.IdUsuario);

            dgvUsuarios.DataSource = usuarios;
        }



        private void Guardar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"INSERT INTO Usuarios 
                        (NombreUsuario, Clave, NombreCompleto, Correo, IdRol, Estado, FechaRegistro)
                         VALUES 
                        (@NombreUsuario, @Clave, @NombreCompleto, @Correo, @IdRol, @Estado, @FechaRegistro)";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@NombreUsuario", txtNombreUsuario.Text);
                cmd.Parameters.AddWithValue("@Clave", txtClave.Text);
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@IdRol", Convert.ToInt32(txtIdRol.Text));
                cmd.Parameters.AddWithValue("@Estado", Convert.ToInt32(txtEstado.Text));
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now); // Recomendado

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario registrado correctamente");
            }
        }

        private void Modificar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = @"UPDATE Usuarios SET 
                        NombreUsuario=@NombreUsuario, 
                        Clave=@Clave, 
                        NombreCompleto=@NombreCompleto, 
                        Correo=@Correo, 
                        IdRol=@IdRol, 
                        Estado=@Estado, 
                        FechaRegistro=@FechaRegistro
                        WHERE IdUsuario=@IdUsuario";

                SqlCommand cmd = new SqlCommand(query, cn);

                // -----------------------------
                // VALIDACIONES DE CAMPOS
                // -----------------------------

                // Validar IdRol
                if (!int.TryParse(txtIdRol.Text.Trim(), out int idRol))
                {
                    MessageBox.Show("El campo IdRol debe contener un número válido.");
                    return;
                }

                // Validar Estado
                if (!int.TryParse(txtEstado.Text.Trim(), out int estado))
                {
                    MessageBox.Show("El campo Estado debe contener un número válido (0 o 1).");
                    return;
                }

                // Validar IdUsuario
                if (!int.TryParse(txtIdUsuario.Text.Trim(), out int idUsuario))
                {
                    MessageBox.Show("El campo IdUsuario no es válido: " + txtIdUsuario.Text);
                    return;
                }

                // -----------------------------
                // AGREGAR PARÁMETROS CORRECTOS
                // -----------------------------

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", txtNombreUsuario.Text);
                cmd.Parameters.AddWithValue("@Clave", txtClave.Text);
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@IdRol", idRol);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);


                // -----------------------------
                // EJECUTAR UPDATE
                // -----------------------------
                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario modificado correctamente");
            }
        }



        private void Eliminar()
        {
            using (SqlConnection cn = new SqlConnection(cad_conexion))
            {
                string query = "DELETE FROM Usuarios WHERE IdUsuario=@IdUsuario";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdUsuario", txtIdUsuario.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado");
            }
        }


        private void btnListar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btbAgregar_Click(object sender, EventArgs e)
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

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                txtIdUsuario.Text = fila.Cells["IdUsuario"].Value.ToString();
                txtNombreUsuario.Text = fila.Cells["NombreUsuario"].Value.ToString();
                txtClave.Text = fila.Cells["Clave"].Value.ToString();
                txtNombreCompleto.Text = fila.Cells["NombreCompleto"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtIdRol.Text = fila.Cells["IdRol"].Value.ToString();
                txtEstado.Text = fila.Cells["Estado"].Value.ToString();
                txtIdUsuario.Enabled = false;

                // ---- CORRECCIÓN IMPORTANTE ----
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

