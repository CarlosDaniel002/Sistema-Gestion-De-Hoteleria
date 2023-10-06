using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class LoginDB
    {
        public static byte[] GetSaltingForUser(int IdUser)
        {
            // Lógica para obtener el salting como un arreglo de bytes

            byte[] salting = new byte[] { 54 }; //  Pendiente modificar toda la lógica del método
            return salting;
        }

        public static Usuario GetUserByUserName(string nombreUsuario)
        {
            // Lógica para obtener un usuario por su nombre de usuario desde la base de datos
            // Esto será reemplazado esto con los datos reales de nuestra base de datos

            // Ejemplo:
            Usuario usuario = new Usuario
            {
                IdUsuario = 1, 
                NombreCompleto = "Usuario",
                NombreUsuario = nombreUsuario, 
                Contraseña = "Password",
                RolUsuario = "Usuario Regular",
                Activo = 1
            };

            return usuario;
        }


    }
}
