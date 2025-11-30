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
                        RazonSocial,
                        RUC,
                        Telefono,
                        Correo,
                        Direccion,
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
                            RazonSocial = dr["RazonSocial"].ToString(),
                            RUC = dr["RUC"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
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
