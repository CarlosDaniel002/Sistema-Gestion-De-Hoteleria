using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class RegiststionInDB
    {
        public static int SetUser(Usuario usuario) {
            // Logica para guardar usuarios.
            // Proceso almacenado para guardar usuarios
            // $"EXEC InsertUsuarios '{usuario.NombreCompleto}', '{usuario.NombreUsuario}', '{usuario.Contraseña}', '{usuario.RolUsuario}'";

            return 1;
        }

        public static void SetTokenForUser(int IdUser, string token)
        {
            // Logica para guardar el token.

            RegiststionInDB.SetTokenForUser(IdUser, token);

        }

        public static void SetSaltingForUser(int IdUser, byte[] salting)
        {
            // Logica para guardar el token.

        }
    }
}
