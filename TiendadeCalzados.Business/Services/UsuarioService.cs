using System;
using System.Collections.Generic;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;

namespace TiendadeCalzados.Business.Services
{
    public class UsuarioService
    {
        private readonly UsuariosDAO usuariosDAO;

        // Constructor
        public UsuarioService()

        {
            usuariosDAO = new UsuariosDAO();
        }

        public List<Usuario> ListarUsuarios(int IdUsuario) 
        {
            return usuariosDAO.ListarUsuarios(IdUsuario);
        }
    }
}
