using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Respuesta
    {

        
        public string Resuesta { set; get; }
        public string Codigo { set; get; }
        public string Descripcion { set; get; }

        public static Respuesta getRespuesta(string R, string C, string D) {
           Respuesta respuesta = new Respuesta();
            
            respuesta.Resuesta = R;
            respuesta.Codigo = C;
            respuesta.Descripcion = D;
            return respuesta;
        }


        
    }
}
