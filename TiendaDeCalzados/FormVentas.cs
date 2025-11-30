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
    public partial class FormVentas : Form
    {
        public FormVentas()
        {
            InitializeComponent();
        }

        public void FormVentas_load(object sender, EventArgs e)
        {
             
                dgvVentas.ColumnCount = 4;
                dgvVentas.Columns[0].Name = "Id";
                dgvVentas.Columns[1].Name = "Fecha";
                dgvVentas.Columns[2].Name = "IdCliente";
                dgvVentas.Columns[3].Name = "Total";

                object[] row1 = { 1, DateTime.Parse("2024-01-05"), 101, 250.50m };
                object[] row2 = { 2, DateTime.Parse("2024-06-15"), 151, 250.50m };
                object[] row3 = { 3, DateTime.Parse("2023-11-25"), 220, 250.50m };
                object[] row4 = { 4, DateTime.Parse("2023-12-09"), 125, 250.50m };

            dgvVentas.Rows.Add(row1);
            dgvVentas.Rows.Add(row2);
            dgvVentas.Rows.Add(row3);
            dgvVentas.Rows.Add(row4);

        }
    }
}


