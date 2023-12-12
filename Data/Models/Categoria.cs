using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public int CantidadMax { get; set; }
        public decimal Precio { get; set; }
        public decimal? Cuota1 { get; set; }
        public decimal? Cuota2 { get; set; }
        public string ComentarioC { get; set; }
        public int? Activo { get; set; }
    }
}
