using Data.Models;
using Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Habitaciones
{
    public class VerHabitaciones
    {
        public static dynamic GetAllHabitaciones()
        {
            var Respuesta = HabitacionDB.GetAllHabitaciones();
            return Respuesta;
        }

        public static dynamic GetAtId(int? Id)
        {
            if (Id != null)
            {
                Habitacion prueba = null;
                var habitacion = HabitacionDB.GetHabitacionAtId(Id);
                if (habitacion == prueba)
                    return Respuesta.getRespuesta("La habitacion no existe", "9970", "El ID que se envia esta nulo o no es valido.");
                else
                    return habitacion;
            }
            else
            {
                return Respuesta.getRespuesta("El Id no es valido.", "9971", "El ID que se envia esta nulo o no es valido.");
            }

        }
    }
}
