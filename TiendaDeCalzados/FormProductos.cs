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
    public partial class FormProductos : Form
    {
        public FormProductos()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void FormProductos_load(object sender, EventArgs e) 
        { 
            dgvProductos.ColumnCount = 8;
            dgvProductos.Columns[0].Name = "Cod";
            dgvProductos.Columns[1].Name = "Nombres";
            dgvProductos.Columns[2].Name = "Marca";
            dgvProductos.Columns[3].Name = "Tallar";
            dgvProductos.Columns[4].Name = "Color";
            dgvProductos.Columns[5].Name = "PrecioCompra";
            dgvProductos.Columns[6].Name = "Stock";
            dgvProductos.Columns[7].Name = "StockMin";

            string[] row1 = { "001", "zapatos Varón", "Calimod", "43", "Negro", "250", "15", "5" };
            string[] row2 = { "002", "zapatos Dama", "Jimmy Choo", "37", "Marron", "200", "18", "7" };
            string[] row3 = { "003", "zapatos Niño", "Milos", "31", "Negro", "50", "12", "7" };
            string[] row4 = { "004", "zapatos Niña", "BonPoint", "29", "Blanco", "150", "10", "3" };

            dgvProductos.Rows.Add(row1);
            dgvProductos.Rows.Add(row2);
            dgvProductos.Rows.Add(row3);
            dgvProductos.Rows.Add(row4);

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
