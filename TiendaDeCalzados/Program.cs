using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiendaDeCalzados
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /* Application.Run(new FormProductos()); 
               Application.Run(new FormVentas());            
               Application.Run(new FormDetalleVentas());
                Application.Run(new FormLogin());
                Application.Run(new Form1());
                Application.Run(new FormLogin());
            */
           // Application.Run(new Form1());
            Application.Run(new FormLogin());


        }
    }
}
