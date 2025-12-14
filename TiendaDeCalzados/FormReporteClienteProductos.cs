using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TiendadeCalzados.Dto;

namespace TiendaDeCalzados
{
    public partial class FormReporteClienteProductos : Form
    {
        public FormReporteClienteProductos()
        {
            InitializeComponent();
        }

        private void FormReporteClienteProductos_Load(object sender, EventArgs e)
        {
            List<ReporteClienteProductosDto> reporte = new List<ReporteClienteProductosDto>
                {
                    new ReporteClienteProductosDto {
                        IdCliente = 1,
                        NombreUsuario = "Maritza",
                        Apellidos = "Mendoza Benavente",
                        ProductosComprados = "Zapatos, Jeans, Blusas"

                    },
                    new ReporteClienteProductosDto {
                        IdCliente = 2,
                        NombreUsuario = "Rosa María",
                        Apellidos = "Salazar Vargas",
                        ProductosComprados = "Casaca, Polo, Bufanda"
                    },

                      new ReporteClienteProductosDto {
                        IdCliente = 3,
                        NombreUsuario = "Paola Andrea",
                        Apellidos = "Sanchez Poma",
                        ProductosComprados = "Falda, Short, Media"
                    },

                };

            ReportDataSource rds = new ReportDataSource("DataSetReporteClienteProductos", reporte);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.LocalReport.ReportPath = "Reportes.rdlc";

            ReportParameter NombreClienteParametro = new ReportParameter("NombreClienteParametro", "Maritza Mendoza Benavente");
            reportViewer1.LocalReport.SetParameters(new ReportParameter[]{
                NombreClienteParametro,

            });


            this.reportViewer1.RefreshReport();
        }
    }
}
