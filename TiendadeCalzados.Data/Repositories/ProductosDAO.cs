using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;


namespace TiendadeCalzados.Data.Repositories
{
    public class ProductosDAO
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"
                    SELECT 
                        IdProducto,
                        NombreProducto,
                        Marca,
                        Talla,
                        PrecioCompra,
                        PrecioVenta,
                        Stock,
                        IdProveedor,
                        FechaRegistro
                    FROM Productos";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Producto prod = new Producto
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            NombreProducto = dr["NombreProducto"].ToString(),
                            Marca = dr["Marca"].ToString(),
                            Talla = dr["Talla"].ToString(),
                            PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                            PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                        };

                        lista.Add(prod);
                    }
                }
            }

            return lista;
        }
    }
}
