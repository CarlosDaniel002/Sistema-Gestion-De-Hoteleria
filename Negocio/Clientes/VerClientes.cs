using Data.Models;
using Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Clientes
{
    public class VerClientes
    {
        public static dynamic GetAllClientes()
        {
            var Respuesta = ClientesDB.GetAllClientes();
            return Respuesta;
        }

        public static dynamic GetAtId(int? Id) 
        {
            if (Id != null)
            {
                Cliente prueba = null;
                var cliente = ClientesDB.GetClienteAtId(Id);
                if (cliente == prueba)
                    return Respuesta.getRespuesta("El cliente no existe", "9970", "El ID que se envia esta nulo o no es valido.");
                else
                    return cliente;
            }
            else
            {
                return Respuesta.getRespuesta("El Id no es valido.", "9971", "El ID que se envia esta nulo o no es valido.");
            }
            
        }
    }

}
