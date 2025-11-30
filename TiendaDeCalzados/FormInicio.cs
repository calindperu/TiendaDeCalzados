using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiendaDeCalzados
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
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

