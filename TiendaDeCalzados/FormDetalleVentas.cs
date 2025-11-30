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
    public partial class FormDetalleVentas : Form
    {
        public FormDetalleVentas()
        {
            InitializeComponent();
        }

        public void FormDetalleVentas_Load(object sender, EventArgs e)
        {
            dgvDetalleVentas.ColumnCount = 6;
            dgvDetalleVentas.Columns[0].Name = "Id";
            dgvDetalleVentas.Columns[1].Name = "IdVenta";
            dgvDetalleVentas.Columns[2].Name = "IdProducto";
            dgvDetalleVentas.Columns[3].Name = "Cantidad";
            dgvDetalleVentas.Columns[4].Name = "PrecioUnitario";
            dgvDetalleVentas.Columns[5].Name = "Subtotal";

         
            string[] row1 = { "13", "1006", "20", "2", "40.00", "80.00" };
            string[] row2 = { "14", "1006", "21", "1", "200.00", "200.00" };
            string[] row3 = { "15", "1007", "22", "3", "10.00", "30.00" };
            string[] row4 = { "16", "1007", "23", "4", "25.00", "100.00" };
            string[] row5 = { "17", "1008", "10", "3", "40.00", "20.00" };
            string[] row6 = { "18", "1009", "11", "4", "15.00", "120.00" };

            dgvDetalleVentas.Rows.Add(row1);
            dgvDetalleVentas.Rows.Add(row2);
            dgvDetalleVentas.Rows.Add(row3);
            dgvDetalleVentas.Rows.Add(row4);
            dgvDetalleVentas.Rows.Add(row5);
            dgvDetalleVentas.Rows.Add(row6);
  

        }
    }
}
