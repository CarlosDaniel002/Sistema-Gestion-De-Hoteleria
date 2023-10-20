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
    public class LoginFunctions
    {
        public static string LoginUser(string nombreUsuario, string contraseña)
        {
            // Buscar al usuario por nombre de usuario en la base de datos
            Usuario usuario = LoginDB.GetUserByUserName(nombreUsuario);

            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            // Obtener el "salting" almacenado para el usuario como una cadena
            string salting = LoginDB.GetSaltingForUser(usuario.IdUsuario);

            // Verificar si la contraseña proporcionada coincide con la almacenada en la base de datos
            string hashContraseña = GetPassword.GenerateHash(contraseña, salting);

            if (hashContraseña != usuario.ContraseñaHash)
            {
                return "Contraseña incorrecta";
            }

            // Generar y devolver un nuevo token de autenticación para el usuario
            string token = Token.GetTokenForIdUser(usuario.IdUsuario);

            return token;
        }
    }
}
