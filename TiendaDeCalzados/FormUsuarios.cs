using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormUsuarios : Form
    {
        private readonly UsuarioService usuarioService;

        public FormUsuarios()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            // CONFIGURAR COLUMNAS DEL GRID
            //dgvUsuarios.ColumnCount = 7;
            dgvUsuarios.AutoGenerateColumns = false;

            dgvUsuarios.Columns.Add("IdUsuario","Codigo de usuario");
            dgvUsuarios.Columns["IdUsuario"].DataPropertyName = "IdUsuario";

            dgvUsuarios.Columns.Add("NombreUsuario", "Nombre de Usuario");
            dgvUsuarios.Columns["NombreUsuario"].DataPropertyName = "NombreUsuario";


            dgvUsuarios.Columns.Add("NombreCompleto", "Datos del Usuario");
            dgvUsuarios.Columns["NombreCompleto"].DataPropertyName = "NombreCompleto";

            dgvUsuarios.Columns.Add("Correo", "Email del usuario");
            dgvUsuarios.Columns["Correo"].DataPropertyName = "Correo";

            dgvUsuarios.Columns.Add("Estado", "Estado");
            dgvUsuarios.Columns["Estado"].DataPropertyName = "Estado";

            dgvUsuarios.Columns.Add("IdRol", "Rol de usuario");
            dgvUsuarios.Columns["IdRol"].DataPropertyName = "IdRol";


            List<Usuario> usuarios = usuarioService.ListarUsuarios(1);
         
            dgvUsuarios.DataSource = usuarios;

        }
    }
}
