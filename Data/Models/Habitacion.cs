using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }
        public int IdCategoria { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public string ComentarioH { get; set; }
        public int Activo { get; set; }
    }
}
