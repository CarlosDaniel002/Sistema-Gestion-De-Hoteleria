using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.UserData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Negocio.Clientes
{
    public class ModificarClientes
    {
        public static Respuesta IngresarCliente(Cliente cliente)
        {
            return ClientesDB.SetCliente(cliente);
        }

        public static Respuesta AlterarCliente(Cliente cliente)
        {
            return ClientesDB.UpdateCliente(cliente);
        }

        public static Respuesta EliminarCliente(int id,int Accion ) {
            if (Accion == 0 || Accion == 1)
            {
                return ClientesDB.DeleteCliente(id, Accion);

            }
            else
            {
                return Respuesta.getRespuesta("La opción seleccionada es incorrecta.", "0402", "El número de ación esta fuera del rango.");
            }
        }
    }
}
