using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class ClientesDAO
    {
        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"SELECT 
                                IdCliente,
                                Nombres,
                                ApellidoPaterno,
                                ApellidoMaterno,
                                Telefono,
                                Correo,
                                Direccion,
                                FechaRegistro
                            FROM Clientes";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cliente cli = new Cliente();

                        cli.IdCliente = (int)dr["IdCliente"];
                        cli.Nombres = dr["Nombres"].ToString();
                        cli.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                        cli.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                        cli.Telefono = dr["Telefono"].ToString();
                        cli.Correo = dr["Correo"].ToString();
                        cli.Direccion = dr["Direccion"].ToString();
                        cli.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);

                        lista.Add(cli);
                    }
                }
            }

            return lista;
        }
    }
}
