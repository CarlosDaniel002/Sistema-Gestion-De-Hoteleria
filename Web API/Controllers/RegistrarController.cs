using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Negocio.User;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrarController : Controller
    {
        [HttpPost]
        public dynamic Registrar(Usuario usuario) {
            return RegistrationFunctions.RegisterUser(usuario); 
        }

    }
}
