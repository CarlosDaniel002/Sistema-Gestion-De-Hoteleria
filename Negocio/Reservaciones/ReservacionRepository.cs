using Data;
using Data.Models;
using Data.UserData;
using Microsoft.EntityFrameworkCore;
using Negocio.Reservaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Recervaciones
{

    public class ReservacionNegocio 
    {
        private static connectionsDB conexion = new connectionsDB();
        public static dynamic ObtenerTodasLasReservaciones()
        {
            return ReservacionDB.GetAllReservaciones(conexion);
        }

        public static dynamic ReservacionPorId(int id)
        {
            return ReservacionDB.ObtenerReservacionPorId(id);
        }

        public static dynamic CrearReservacion(Reservacion reservation)
        {
            return ReservacionDB.InsertarReservacion(reservation);
        }

        public static dynamic ActualizarReserva(int id, Reservacion reservation)
        {
            return ReservacionDB.ActualizarReservacion(id, reservation);
        }

        public static dynamic EliminarReserva(int id)
        {
            return ReservacionDB.EliminarReservacion(id);
        }
    }

}
