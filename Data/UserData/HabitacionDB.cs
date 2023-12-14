using Dapper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.UserData
{
    public class HabitacionDB
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand sqlCommand;
        private static SqlDataReader reader;
        private static int commandTimeoutInSeconds = 30;
        private static string Query;


        public static dynamic GetAllHabitaciones()
        {
            try
            {

                List<Habitacion> ListaHabitacion = new List<Habitacion>();
                Task.Run(() =>
                {
                    conexion.abrirConexion();
                }).Wait(TimeSpan.FromSeconds(30));


                Query = $"Select * from Habitaciones;";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                sqlCommand.CommandTimeout = commandTimeoutInSeconds;
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(5)))
                    {
                        ListaHabitacion.Add(new Habitacion()
                        {
                            IdHabitacion = reader.GetInt32(0),
                            IdCategoria = reader.GetInt32(1),
                            Ubicacion = reader.GetString(2),
                            Estado = reader.GetString(3),
                            ComentarioH = reader.GetString(4),
                            Activo = reader.GetByte(5)
                        });
                    }
                }
                conexion.cerrarConexion();
                Console.WriteLine($"Se cargo una lista de {ListaHabitacion.Count} - Habitaciones.");
                return ListaHabitacion;
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

        public static dynamic GetHabitacionAtId(int? Id)
        {
            try
            {
                Habitacion HabitacionID = null;
                conexion.abrirConexion();
                Query = $"Select * from Habitaciones where IdHabitacion = {Id};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(5)))
                    {
                        HabitacionID = new Habitacion()
                        {
                            IdHabitacion = reader.GetInt32(0),
                            IdCategoria = reader.GetInt32(1),
                            Ubicacion = reader.GetString(2),
                            Estado = reader.GetString(3),
                            ComentarioH = reader.GetString(4),
                            Activo = reader.GetByte(5)
                        };
                    }
                }
                conexion.cerrarConexion();
                return HabitacionID;
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", "9990", $"{Error}");
            }
        }


        public static dynamic SetHabitacion(Habitacion habitacion)
        {
            try
            {

                using (var connection = new SqlConnection(conexion.dataBase.ConnectionString))
                {
                    connection.Open();
                    // var query = "EXEC ActualizarClave @Nuevo = @Clave, @ID = @IDU; ";
                    var query = "EXEC InsertHabitacion @IDCategoria, @HUbicacion, @Comentario; ";
                    var parameters = new { IDCategoria = habitacion.IdCategoria, HUbicacion = habitacion.Ubicacion, Comentario =  habitacion.ComentarioH };
                    connection.QueryFirstOrDefault(query, parameters);
                }

                Console.WriteLine($"Nuevo estado de {habitacion.Estado} - habitacion.");
                return Respuesta.getRespuesta("Se genero con exito", "0000", "");
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", "9990", $"{Error}");
            }
        }

        public static Respuesta UpdateHabitacion(Habitacion habitacion)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"EXEC ActualizarHabitaciones @Id= {habitacion.IdHabitacion}, @IdCategoria = {habitacion.IdCategoria}, @Ubicacion = '{habitacion.Ubicacion}', " +
                    $"@Estado = '{habitacion.Estado}', @ComentarioH = '{habitacion.ComentarioH}';";
                // Query = $"EXEC UpdateHabitacion '{habitacion.Ubicacion}', '{habitacion.Estado}', '{habitacion.ComentarioH}';";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Actualización de registro de {habitacion.Estado} - .");
                return Respuesta.getRespuesta("Se actualizo.", "0000", "");
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", "9990", $"{Error}");
            }
        }

        public static dynamic DeleteHabitacion(int Id, int activar)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"Update Habitaciones set Activo = {activar} where IdHabitacion = {Id};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();
                Console.WriteLine($"Se elimino el id {Id} - habitacion.");
                return activar == 0 ? Respuesta.getRespuesta("Se elimino exitosamente.", "0000", "") : Respuesta.getRespuesta("Se activo exitosamente.", "0000", "");
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", "9990", $"{Error}");
            }

        }
    }
}
