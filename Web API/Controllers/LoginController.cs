using Microsoft.AspNetCore.Mvc;
using Negocio.Clientes;
using Negocio.User;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public dynamic Get(string nombreUsuario, string clave)
        {
            var Respuesta = LoginFunctions.LoginUser(nombreUsuario,clave);
            return Respuesta;
        }






    }
}
