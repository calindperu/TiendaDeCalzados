using System;
using System.Drawing;
using System.Windows.Forms;
using TiendadeCalzados.Entities;
using TiendadeCalzados.Presentation;
using System.Collections.Generic;
using TiendadeCalzados.Business.Services;


namespace TiendaDeCalzados
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void FormInicio_Load(object sender, EventArgs e) 
        {
            AbrirFormEnPanel(new FormBienvenida());
            CargarRoles();
        }

        private void AbrirFormEnPanel(Form formHijo)
        { 
            if (pnlContenedor.Controls.Count > 0) 
            
            {
                pnlContenedor.Controls.RemoveAt(0);
            }
                formHijo.TopLevel = false;
                formHijo.WindowState = FormWindowState.Maximized;
                formHijo.FormBorderStyle = FormBorderStyle.None;
                formHijo.Dock = DockStyle.Fill;
                pnlContenedor.Controls.Add(formHijo);
                formHijo.Show();
        }

        private void CargarRoles() {
            Usuario Usuario = SesionActual.UsuarioLogueado;

            // Rol Administrador
            if (Usuario.IdRol == 1) 
            { 
               btnClientes.Enabled = true;
               btnProductos.Enabled = true;
               btnVentas.Enabled = true;
               btnDetalleVentas.Enabled = true;
               btnProveedores.Enabled = true;
               btnReportes.Enabled = true;
               btnUsuarios.Enabled = true;              

            }

            // Rol Vendedor
            if (Usuario.IdRol == 2)
            {
                btnClientes.BackColor = Color.LightGray;             
                btnProductos.BackColor = Color.LightGray;

                btnVentas.Enabled = true;
                btnDetalleVentas.Enabled = true;

                btnProveedores.BackColor = Color.LightGray;
                btnReportes.BackColor = Color.LightGray;
                btnUsuarios.BackColor = Color.LightGray;

            }

            // Rol Almacenero
            if (Usuario.IdRol == 3)
            {
                btnClientes.BackColor = Color.LightGray;
                btnVentas.BackColor = Color.LightGray;
                btnDetalleVentas.BackColor = Color.LightGray;
                btnProveedores.BackColor = Color.LightGray;

                btnProductos.Enabled = true;
                btnReportes.Enabled = true;

                btnUsuarios.BackColor = Color.LightGray;

            }

            // Rol Proveedor
            if (Usuario.IdRol == 4)
            {
                btnClientes.BackColor = Color.LightGray;

                btnProductos.Enabled = true;

                btnVentas.BackColor = Color.LightGray;
                btnDetalleVentas.BackColor = Color.LightGray;
                btnProveedores.BackColor = Color.LightGray;
                btnReportes.BackColor = Color.LightGray;
                btnUsuarios.BackColor = Color.LightGray;

            }

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormClientes());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormProductos());

        }

       private void btnClientes_Click_1(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormClientes());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormVentas());
        }

        private void btnDetalleVentas_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormDetalleVentas());
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormProveedores());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormUsuarios());
        }

                private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new FormReportes());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

