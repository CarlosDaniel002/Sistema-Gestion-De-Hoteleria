using Data.Models;
using Data.UserData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Negocio.User
{
    public class ActualizarContr
    {
        public static dynamic ActualizarContrasena(int idUsuario, string nombreUsuario, string nuevaContrasena, string confirmarContrasena, Guid nuevoToken)
        {


            try
            {
                // Verificar si las contraseñas coinciden
                if (nuevaContrasena != confirmarContrasena)
                {
                    return Respuesta.getRespuesta("Las contraseñas no coinciden.", "0000", "");
                }

                // Obtener el salting actual del usuario
                string saltingActual = ObtenerSaltingActual(idUsuario);

                // Generar el nuevo hash de la contraseña con el salting actual
                string nuevoHashContraseña = GetPassword.GenerateHash(nuevaContrasena, GetPassword.StringToByteArray(saltingActual));

                // Actualizar la contraseña y el token del usuario (sin modificar el salting)
                ActualizarContraseña.SetUpdateTokenForUser(idUsuario, nuevoToken, nuevoHashContraseña);

                return Respuesta.getRespuesta("Contraseña y token actualizados con éxito.", "0000", "");
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.", $"{Error.Number}", $"{Error.Errors}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", $"{Error.HResult}", $"{Error.Message} ");
            }
        }

        public static string ObtenerSaltingActual(int idUsuario)
        {
            // Obtener el salting actual del usuario desde la base de datos
            return ActualizarContraseña.ObtenerSaltingActual(idUsuario);
        }

        }
    }
