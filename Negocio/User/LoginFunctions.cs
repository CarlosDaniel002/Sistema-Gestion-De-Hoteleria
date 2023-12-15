using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.UserData;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static dynamic ChangePassword(string token, string contraseña, string claveNueva, string claveNuevaCon)
        {
            LoginDB loginDB = new LoginDB();
            Usuario usuario = loginDB.GetUserByUserToken(token);

            if (usuario == null)
            {
                return Respuesta.getRespuesta("El usuario no existe.", "9998", $"El token provisto no es valido") ;
            }

            string salting = loginDB.GetSaltingForUser(usuario.IdUsuario);

            string hashContraseña = GetPassword.GenerateHash(contraseña, GetPassword.StringToByteArray(salting));

            if (hashContraseña == usuario.ContrasenaHash && claveNueva == claveNuevaCon)
            {
                // Llamar al metodo para cambiar la clave 
                string nuevoHash = GetPassword.GenerateHash(claveNueva, GetPassword.StringToByteArray(salting));
                loginDB.SetNewPasswordByIdUser(usuario.IdUsuario, nuevoHash);

                Guid nuevoToken = Token.GetUpdateTokenDB(usuario.IdUsuario);
                return nuevoToken.ToString();
            }
            else
            {
                return Respuesta.getRespuesta("Contraseña incorrecta.", "9990", $"Las calves estan erroneas");
            }
        }
    }
}
