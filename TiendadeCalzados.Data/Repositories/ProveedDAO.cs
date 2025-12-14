using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class ProveedDAO
    {
        public List<Proveedor> ListarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"
                    SELECT 
                        IdProveedor,
                        RUC,
                        RazonSocial,
                        Telefono,
                        Correo,
                        Direccion,
                        Estado,
                        FechaIngreso
                    FROM Proveedores";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Proveedor prov = new Proveedor
                        {
                            IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                            RUC = dr["RUC"].ToString(),
                            RazonSocial = dr["RazonSocial"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Direccion = dr["Direccion"].ToString(),

                            // Si Estado es BIT en SQL Server
                            Estado = Convert.ToBoolean(dr["Estado"]),

                            FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"])
                        };

                        lista.Add(prov);
                    }
                }
            }

            return lista;
        }
    }
}

