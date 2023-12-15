using Microsoft.AspNetCore.Mvc;
using Negocio.Clientes;
using Negocio.User;
using Data.Models;
using System;
using System.Collections.Generic;

namespace Web_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public dynamic Login(LoginUser user)
        {
            var Respuesta = LoginFunctions.LoginUser(user.Usuario, user.Clave);
            return Respuesta;
        }

        [HttpPut("login")]
        public dynamic ChangePassword(Password clave)
        {
            var Respuesta = LoginFunctions.ChangePassword(clave.token,clave.claveActual,clave.claveNueva,clave.claveNuevaCon);
            return Respuesta;
        }
    }
}