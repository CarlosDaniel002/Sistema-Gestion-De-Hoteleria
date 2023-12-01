using Microsoft.AspNetCore.Mvc;
using Negocio.Clientes;
using Negocio.User;
using System.Diagnostics;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class LoginController : Controller
    {
        private static object GetDebuggerDisplay()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public dynamic Get(string nombreUsuario, string contraseña)
        {
            var Respuesta = LoginFunctions.LoginUser(nombreUsuario,contraseña);
            return Respuesta;
        }
    }

    namespace Web_API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            // GET: api/<LoginController>
            [HttpGet]
            public IEnumerable<string> Get()
            {
                return new string[] { "value1", "value2" };
            }

            // GET api/<LoginController>/5
            [HttpGet("{id}")]
            public string Get(int id)
            {
                return "value";
            }


            // POST api/<LoginController>
            [HttpPost]
            public void Post([FromBody] string value)
            {
            }

            // PUT api/<LoginController>/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }


        }
    }
}
