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
    public partial class FormReportes : Form
    {
        public FormReportes()
        {
            InitializeComponent();
        }

        private void FormDetalleVentas_Load(object sender, EventArgs e)
        {
            // Crear columnas MANUALMENTE
            dgvReportes.Columns.Add("IdReporte", "IdReporte");
            dgvReportes.Columns.Add("TipoReporte", "TipoReporte");
            dgvReportes.Columns.Add("FechaGeneracion", "FechaGeneracion");
            dgvReportes.Columns.Add("Detalle", "Detalle");
            dgvReportes.Columns.Add("IdUsuario", "IdUsuario");

            // Agregar filas manuales (simulando datos)
            dgvReportes.Rows.Add(1, "Reporte de Ventas", "2025-11-16", "Detalle del reporte 1", 3);
            dgvReportes.Rows.Add(2, "Reporte de Productos", "2025-11-15", "Detalle del reporte 2", 2);
            dgvReportes.Rows.Add(3, "Reporte de Usuarios", "2025-11-14", "Detalle del reporte 3", 1);
        }
    }
}
