using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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