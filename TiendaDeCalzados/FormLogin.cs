using System;
using System.Windows.Forms;
using TiendadeCalzados.Business.Services;
using TiendadeCalzados.Entities;


namespace TiendaDeCalzados
{
    public partial class FormLogin : Form
    {
        private readonly LoginService loginService;
        public FormLogin()
        {
            InitializeComponent();
            loginService = new LoginService();

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            try
            {
                Usuario usuario = loginService.Login(txtUsuario.Text, txtClave.Text);

                SesionActual.UsuarioLogueado = usuario;

                MessageBox.Show(
                    "Bienvenido " + usuario.NombreCompleto + " a la Tienda de Calzados",
                    "Acceso OK",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                FormInicio formInicio = new FormInicio();
                formInicio.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Acceso:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

    }
}
