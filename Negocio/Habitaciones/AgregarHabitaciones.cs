using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.UserData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Negocio.Habitaciones
{
    public class AgregarHabitaciones
    {
        public static Respuesta IngresarHabitacion(Habitacion habitacion)
        {
            return HabitacionDB.SetHabitacion(habitacion);
        }

        public static Respuesta AlterarHabitacion(Habitacion habitacion)
        {
            return HabitacionDB.UpdateHabitacion(habitacion);
        }

        public static Respuesta EliminarHabitacion(int id, int Accion)
        {
            if (Accion == 0 || Accion == 1)
            {
                return HabitacionDB.DeleteHabitacion(id, Accion);
            }
            else
            {
                return Respuesta.getRespuesta("La opción seleccionada es incorrecta.", "0402", "El número de ación esta fuera del rango.");
            }
        }
    }
}
