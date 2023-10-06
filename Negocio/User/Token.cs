using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.User
{
    public class Token
    {
        private static dynamic? varToken{ set; get; }
        public static dynamic GetRandomTokenAndKeepIt(int IdUsuario)
        {
            varToken = Guid.NewGuid().ToString();

            // Se genera el token y se guarda, falta la logica de guardado con el ID 

            return varToken;
        }

        public static dynamic GetTokenForIdUser(int IdUsuarios)
        {
            // Logica para obtener el Token del usuario por Id
            // Modificar logica al tener DB
            varToken = Guid.NewGuid().ToString();
            
            return varToken;
        }
    }
}
