using Data.Models;
using Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Categorias
{
    public class VerCategorias
    {
        public static dynamic GetAllCategorias()
        {
            var Respuesta = CategoriasDB.GetAllCategorias();
            return Respuesta;
        }

        public static dynamic GetAtId(int? Id)
        {
            if (Id != null)
            {
                Categoria prueba = null;
                var categoria = CategoriasDB.GetCategoriaAtId(Id);
                if (categoria == prueba)
                    return Respuesta.getRespuesta("La categoría no existe", "9970", "El ID que se envía está nulo o no es válido.");
                else
                    return categoria;
            }
            else
            {
                return Respuesta.getRespuesta("El Id no es válido.", "9971", "El ID que se envía está nulo o no es válido.");
            }
        }
    }
}

