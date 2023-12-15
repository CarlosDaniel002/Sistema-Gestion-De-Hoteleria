using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Password
    {
        public string token { get; set; }
        public string claveActual { get; set; }
        public string claveNueva { get; set; }
        public string claveNuevaCon { get; set; }
    }
}
