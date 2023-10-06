using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entidades;
using Data.UserData;
using System.Globalization;

namespace Negocio.User
{
    public class RegistrationFunctions
    {
        public string RegisterUser(Usuario usuario)
        {
            string Respuesta;
            // Logica para registro
            if (usuario.NombreCompleto == null)
                return "Nombre nulo.";
            if (usuario.NombreUsuario == null)
                return "Usuario nulo.";
            if (usuario.Contraseña == null)
                return "Contraseña nula.";
            if (usuario.RolUsuario == null)
                return "Rol nulo.";
            else 
            {
                // Se genera el hash
                byte[] sal = GetPassword.GenerateSalting();
                usuario.Contraseña = GetPassword.GenerateHash(usuario.Contraseña, sal); 
                
                // Se guarda la data
                int IdUser = RegiststionInDB.SetUser(usuario);
                // Guadar el Salting por el Id
                RegiststionInDB.SetSaltingForUser(IdUser, sal);
                // Guardar Token con Id
                string token = Token.GetRandomTokenAndKeepIt(IdUser);
                return token;
            }
        }
    }
}
