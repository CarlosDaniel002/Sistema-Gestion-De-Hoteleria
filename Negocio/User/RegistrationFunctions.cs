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
    public class RegistrationFunctions
    {
        public static dynamic RegisterUser(Usuario usuario)
        {
            Guid token = Token.GetRandomTokenAndKeepIt();
            string Respuesta;
            // Logica para registro
            if (usuario.NombreCompleto == null)
                return "Nombre nulo.";
            if (usuario.NombreUsuario == null)
                return "Usuario nulo.";
            if (usuario.ContrasenaHash == null)
                return "Contraseña nula.";
            if (usuario.RolUsuario == null)
                return "Rol nulo.";
            else 
            {
                // Se genera el hash
                byte[] sal = GetPassword.GenerateSalting();
                usuario.ContrasenaHash = GetPassword.GenerateHash(usuario.ContrasenaHash, sal);
                usuario.Salting = BitConverter.ToString(sal).Replace("-", "");
                // Se guarda la data
                var TokenO = RegiststionInDB.SetUser(usuario, token);
                return TokenO;
            }
        }
    }
}
