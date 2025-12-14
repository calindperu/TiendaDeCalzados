using System.Data.SqlClient;

using System;




namespace TiendadeCalzados.Data.Connection
{
    public class ConnectionDB
    {   // PC Giovanna
        //private static readonly string connectioString = "Server=Localhost\\SQL2022EXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";

        //PC Carlos
        private static readonly string connectioString = "Server=Localhost\\SLQEXPRESS;Database=TIENDACALZADOS;Trusted_Connection=True";


        // PC Trabajo
        //string cad_conexion = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;";
        //private static readonly string connectioString = "Server=Localhost\\SQLEXPRESS;Database=TIENDACALZADOS;User Id=sa;Password=carlos$18;Trusted_Connection=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectioString);
        }

    }
}
