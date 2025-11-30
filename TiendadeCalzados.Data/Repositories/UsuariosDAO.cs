using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiendadeCalzados.Data.Connection;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Data.Repositories
{
    public class UsuariosDAO
    {
        // Devuelve todos los usuarios si IdUsuario == 0,
        // o el usuario específico si IdUsuario > 0
        public List<Usuario> ListarUsuarios(int IdUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection cn = ConnectionDB.GetConnection())
            {
                string sql = @"
                        SELECT 
                            U.IdUsuario,
                            U.NombreUsuario,
                            U.Clave,
                            U.NombreCompleto,
                            U.Correo,
                            U.IdRol,
                            U.Estado,
                            R.NombreRol
                        FROM Usuarios U
                        INNER JOIN Roles R ON R.IdRol = U.IdRol                         
                        ";



                /*
                 
                            SELECT 
                                U.IdUsuario,
                                U.NombreUsuario,
                                U.NombreCompleto,
                                U.Correo,
                                U.Estado,
                                U.IdRol,
                                R.NombreRol
                            FROM Usuarios U
                            INNER JOIN Roles R ON U.IdRol = R.IdRol
                            WHERE U.NombreUsuario = @NombreUsuario
                              AND U.Clave = @Clave
                              AND U.Estado = 1;  
                  
                        SELECT U.*, R.NombreRol
                            FROM Usuarios U
                            INNER JOIN Roles R ON U.IdRol = R.IdRol
                            WHERE (@IdUsuario = 0 OR U.IdUsuario = @IdUsuario)
                            ORDER BY U.IdUsuario

                 */

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreUsuario = dr["NombreUsuario"]?.ToString(),
                                NombreCompleto = dr["NombreCompleto"]?.ToString(),
                                Correo = dr["Correo"]?.ToString(),
                                // Si usas BIT en BD, puedes mapearlo a int o bool según tu clase
                                Estado = dr["Estado"] != DBNull.Value ? Convert.ToInt32(dr["Estado"]) : 0,
                                IdRol = dr["IdRol"] != DBNull.Value ? Convert.ToInt32(dr["IdRol"]) : 0,
                                Rol = new Rol
                                {
                                    IdRol = dr["IdRol"] != DBNull.Value ? Convert.ToInt32(dr["IdRol"]) : 0,
                                    NombreRol = dr["NombreRol"]?.ToString()
                                }
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}
