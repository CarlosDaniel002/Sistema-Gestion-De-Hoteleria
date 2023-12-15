using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Reservacion
    {
        public int IdReservacion { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime FechaNow { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public TimeSpan? HoraCheckIn { get; set; }
        public TimeSpan? HoraCheckOut { get; set; }
        public string Estado { get; set; }
        public string EstadoPago { get; set; }
        public string MetodoPagoC1 { get; set; }
        public string ComentarioR { get; set; }
        public int CantidadHuesped { get; set; }
       
      
        
    }

}

