using System;
using System.Windows.Forms;
using TiendadeCalzados.Entities;

namespace TiendaDeCalzados
{
    public partial class FormBienvenida : Form
    {
        public FormBienvenida()
        {
            InitializeComponent();
        }

        private void FormBienvenida_Load(object sender, EventArgs e)
        {
            Usuario usuario = SesionActual.UsuarioLogueado;
            lblBienvenido.Text = "¡Bienvenido usuario " + usuario.NombreCompleto + " a la Tienda de Calzados!";

        }
    }
}
