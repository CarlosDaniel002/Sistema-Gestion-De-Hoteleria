using Microsoft.AspNetCore.Mvc;
using Negocio.Clientes;
using Negocio.User;
using System;
using System.Collections.Generic;

namespace Web_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("login")]
        public dynamic Login(string nombreUsuario, string contraseña)
        {
            var Respuesta = LoginFunctions.LoginUser(nombreUsuario, contraseña);
            return Respuesta;
        }
    }
}