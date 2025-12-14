using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TiendaDeCalzados
{
    public partial class FormProbarConexion : Form
    {
        public FormProbarConexion()
        {
            InitializeComponent();
        }


        private void ProbarConexion()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(
                    @"Server=.\SQLEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True;"))
                {
                    cn.Open();
                    MessageBox.Show("✅ Conexión exitosa a SQL Server");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ ERROR:\n" + ex.Message);
            }
        }
    }
}