using System;
using TiendadeCalzados.Data.Repositories;
using TiendadeCalzados.Entities;
using Microsoft.SqlServer.Server;



namespace TiendadeCalzados.Business.Services
{
    public class LoginService
    {
        //private readonly LoginDAO loginDAO;
        private readonly LoginDAO loginDAO;

        // Creamos el Constructor para inicializar un dato o variable
        public LoginService()
        {
            loginDAO = new LoginDAO();
        }

        // creamos el medotodo de la ejecucion
        public Usuario Login(string NombreUsuario, string Clave)
        {
            if (string.IsNullOrWhiteSpace(NombreUsuario))
            {
                throw new Exception("Debe ingresar un nombre de usuario");
            }
            if (string.IsNullOrWhiteSpace(Clave))
            {
                throw new Exception("Debe ingresar su clave de accceso");
            }

            Usuario usuario = loginDAO.Login(NombreUsuario, Clave);

            if (usuario == null)
            {
                throw new Exception("Usuario o Clave son incorrectos");
            }

            return usuario;
        }
    }
}
