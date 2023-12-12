using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.UserData;
using System.Globalization;

namespace Negocio.User
{
    public class LoginFunctions
    {
        public static string LoginUser(string nombreUsuario, string contraseña)
        {
            LoginDB loginDB = new LoginDB();
            Usuario usuario = loginDB.GetUserByUserName(nombreUsuario);

            if (usuario == null)
            {
                return "El usuario no existe.";
            }
            
            string salting = loginDB.GetSaltingForUser(usuario.IdUsuario);

            string hashContraseña = GetPassword.GenerateHash(contraseña, GetPassword.StringToByteArray(salting));

            if (hashContraseña == usuario.ContrasenaHash)
            {
                Guid token = Token.GetUpdateTokenDB(usuario.IdUsuario);
                return token.ToString();
            }
            else
            {
                return "Contraseña incorrecta.";
            }
        }
    }
}
