using Data.Models;
using Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Categorias
{
    public class GestionarCategorias
    {
        public static Respuesta IngresarCategoria(Categoria categoria)
        {
            return CategoriasDB.SetCategoria(categoria);
        }

        public static Respuesta ActualizarCategoria(Categoria categoria)
        {
            return CategoriasDB.UpdateCategoria(categoria);
        }

        public static Respuesta EliminarCategoria(int id, int Accion)
        {
            if (Accion == 0 || Accion == 1)
            {
                return CategoriasDB.DeleteCategoria(id, Accion);
            }
            else
            {
                return Respuesta.getRespuesta("La opción seleccionada es incorrecta.", "0402", "El número de acción está fuera del rango.");
            }
        }
    }
}
