using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Negocio.Categorias;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : Controller
    {
        [HttpGet]
        public dynamic Get()
        {
            var respuesta = VerCategorias.GetAllCategorias();
            return respuesta;
        }

        [HttpGet]
        [Route("GetId")]
        public dynamic GetAtId(int? Id)
        {
            var respuesta = VerCategorias.GetAtId(Id);
            return respuesta;
        }

        [HttpPost]
        public Respuesta CrearCategoria(Categoria categoria)
        {
            Respuesta mensaje = GestionarCategorias.IngresarCategoria(categoria);
            return mensaje;
        }

        [HttpPut]
        public Respuesta ModificarCategoria(Categoria categoria)
        {
            Respuesta mensaje = GestionarCategorias.ActualizarCategoria(categoria);
            return mensaje;
        }

        [HttpDelete]
        public Respuesta Delete(IdEliminar Id)
        {
            Respuesta mensaje = GestionarCategorias.EliminarCategoria(Id.Id, Id.Accion);
            return mensaje;
        }
    }
}
