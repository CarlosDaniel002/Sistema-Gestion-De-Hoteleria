using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Data.Models;
using Negocio.Habitaciones;
using Microsoft.Graph;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Data.UserData;
using Negocio.Clientes;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HabitacionController : Controller
    {
        [HttpGet]
        public dynamic Get()
        {
            var Respuesta = VerHabitaciones.GetAllHabitaciones();
            return Respuesta;
        }

        [HttpGet]
        [Route("GetId")]
        public dynamic GetAtId(int? Id)
        {
            var Respuesta = VerHabitaciones.GetAtId(Id);
            return Respuesta;
        }

        [HttpPost]

        public Respuesta CrearUsuario(Habitacion habitacion)
        {
            Respuesta mensaje = AgregarHabitaciones.IngresarHabitacion(habitacion);
            return mensaje;
        }

        [HttpPut]
        public Respuesta ModificarUsuario(Habitacion habitacion)
        {
            Respuesta mensaje = AgregarHabitaciones.AlterarHabitacion(habitacion);
            return mensaje;
        }

        [HttpDelete]
        public Respuesta Delete(IdEliminar Id)
        {
            Respuesta mensaje = AgregarHabitaciones.EliminarHabitacion(Id.Id, Id.Activar);
            return mensaje;
        }
    }
}
