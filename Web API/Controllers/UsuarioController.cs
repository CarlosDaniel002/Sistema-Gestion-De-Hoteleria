using Data.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.User;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        [HttpPut("{id}")]
        public IActionResult ActualizarContraseña(int idUsuario, string nombreUsuario, string nuevaContrasena, string confirmarContrasena, Guid nuevoToken)
        {
            try
            {
                // Crear una instancia de UsuarioFunctions (o el nombre que hayas dado a tu clase)
                ActualizarContr actualizarFunctions = new ActualizarContr();

                // Llamar al método ActualizarContrasena
                var resultado = ActualizarContr.ActualizarContrasena(idUsuario, nombreUsuario, nuevaContrasena, confirmarContrasena, nuevoToken);


                // Hacer algo con el resultado, por ejemplo, devolverlo como JSON
                return Json(resultado);
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí, por ejemplo, devolver un mensaje de error
                return Json(new { error = $"Error: {ex.Message}" });
            }
        }




    }
}
