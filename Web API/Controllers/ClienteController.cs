using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Data.Models;
using Negocio.Clientes;
using Microsoft.Graph;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Data.UserData;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        [HttpGet]
        public dynamic Get()
        {
            var Respuesta = VerClientes.GetAllClientes();
            return Respuesta;
        }

        [HttpGet]
        [Route("GetId")]
        public dynamic GetAtId(int? Id)
        {
            var Respuesta = VerClientes.GetAtId(Id);
            return Respuesta;
        }

        [HttpPost]

        public Respuesta CrearUsuario(Cliente cliente)
        {
            Respuesta mensaje = ModificarClientes.IngresarCliente(cliente);
            return mensaje;
        }

        [HttpPut]
        public Respuesta ModificarUsuario(Cliente cliente)
        {
            Respuesta mensaje = ModificarClientes.AlterarCliente(cliente);
            return mensaje;
        }

        [HttpDelete]
        public Respuesta Delete(int Id, int Accion)
        {
            Respuesta mensaje = ModificarClientes.EliminarCliente(Id, Accion);
            return mensaje;
        }
    }
}
