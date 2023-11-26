using Microsoft.AspNetCore.Mvc;
using Negocio.Clientes;
using Negocio.User;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpGet]
        public dynamic Get(string nombreUsuario, string contraseña)
        {
            var Respuesta = LoginFunctions.LoginUser(nombreUsuario,contraseña);
            return Respuesta;
        }
    }
}
