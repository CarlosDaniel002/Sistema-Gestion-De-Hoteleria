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
using Negocio.Recervaciones;
using Negocio.Reservaciones;
using Data;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase

    {

        private static connectionsDB conexion = new connectionsDB();

    // GET: api/Reservaciones
    [HttpGet]
        public dynamic GetAllReservaciones()
        {
            try
            {
                var reservacion = ReservacionDB.GetAllReservaciones(conexion);
                return reservacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET ID: api/Reservaciones/5
        [HttpGet("POR ID")]
        public dynamic GetAtId(int id)
        {
            try
            {
                var reservacion = ReservacionDB.ObtenerReservacionPorId(id);

                if (reservacion == null)
                    return NotFound($"No se encontró ninguna reservación con ID: {id}");

                return reservacion;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Reservaciones
        [HttpPost]
        public dynamic CrearReservacion([FromBody] Reservacion nuevareservacion)
        {
            try
            {
                int idReservacion = ReservacionDB.InsertarReservacion(nuevareservacion);
                return Ok($"Reservación creada con ID: {idReservacion}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // PUT: api/Reservaciones/5
        [HttpPut("{id}")]
        public dynamic ModificarReservacion(int id, [FromBody] Reservacion reservacion)
        {
            try
            {
                ReservacionDB.ActualizarReservacion(id, reservacion);
                return Ok($"Reservación con ID {id} actualizada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Reservaciones/5
        [HttpDelete("{id}")]
        public dynamic EliminarReservacion(int id)
        {
            try
            {
                ReservacionDB.EliminarReservacion(id);
                return Ok($"Reservación con ID {id} eliminada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // EDITAR: api/Reservaciones/5
        [HttpPost("PagarCuota2/{idReservacion}")]
        public IActionResult PagarCuota2(int idReservacion, string metodoPagoC2)
        {
            try
            {

                ReservacionDB.PagarCuota2(idReservacion, metodoPagoC2);


                return Ok($"Cuota 2 pagada para la Reservación con ID {idReservacion}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al pagar la cuota 2: {ex.Message}");
            }
        }
        // EDITAR: api/Reservaciones/1
        [HttpPost("RealizarCheckIn/{idReservacion}")]
        public IActionResult RealizarCheckIn(int idReservacion)
        {
            try
            {

                ReservacionDB.RealizarCheckIn(idReservacion);


                return Ok($"Check-in realizado para la Reservación con ID {idReservacion}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar el Check-in: {ex.Message}");
            }
        }
        // EDITAR: api/Reservaciones/5
        [HttpGet("RealizarCheckOut/{idReservacion}")]
        public IActionResult RealizarCheckOut(int idReservacion)
        {
            try
            {

                ReservacionDB.RealizarCheckOut(idReservacion);


                return Ok($"Check-out realizado para la Reservación con ID {idReservacion}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar el Check-out: {ex.Message}");
            }
        }



    }

}
