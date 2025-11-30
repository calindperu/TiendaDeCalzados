using System;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class LoginDAO
    {
        public Usuario Login(string usuario, string clave)
        {
            Usuario nombreusuario = null;

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"SELECT *
                               FROM Usuarios
                               WHERE NombreUsuario = @NombreUsuario 
                               AND Clave = @Clave";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@NombreUsuario", usuario);
                cmd.Parameters.AddWithValue("@Clave", clave);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    nombreusuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        NombreCompleto = dr["NombreCompleto"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Estado = Convert.ToInt32(dr["Estado"]),
                        IdRol = Convert.ToInt32(dr["IdRol"])   // CORREGIDO
                    };
                }
            }

            return nombreusuario;
        }
    }
}