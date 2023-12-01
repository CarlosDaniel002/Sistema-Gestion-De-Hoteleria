using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
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
=======

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        
>>>>>>> a5fb8fde5a7897f02d7e223410efe552ac01e997
    }
}
