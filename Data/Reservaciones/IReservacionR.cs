using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Reservaciones
{
    public interface IReservacionR
    {
        Task<IEnumerable<Reservacion>> ObtenerReservaciones();
        Task<Reservacion> ObtenerReservacionPorId(int id);
        Task<int> CrearReservacion(Reservacion reservation);
        Task ActualizarReservacion(int id, Reservacion reservation);
        Task EliminarReservacion(int id);
    }
}
