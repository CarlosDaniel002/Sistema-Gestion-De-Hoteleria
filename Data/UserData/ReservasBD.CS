using Data.Models;
using Negocio.Reservaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class ReservacionDB : ReservacionDBBase
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand sqlCommand;
        private static SqlDataReader reader;
        private static int commandTimeoutInSeconds = 30;
        private static string Query;
        
        public static dynamic GetAllReservaciones(connectionsDB conexion)
        {
            try
            {
                List<Reservacion> ListaReservaciones = new List<Reservacion>();

                using (conexion)
                {
                    conexion.abrirConexion();

                    Query = "SELECT * FROM Reservaciones;";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ListaReservaciones.Add(MapearReservacionDesdeDataReader(reader));
                    }

                    // Asegúrate de cerrar el DataReader después de usarlo
                    reader.Close();
                }

                Console.WriteLine($"Se cargó una lista de {ListaReservaciones.Count} - Reservaciones.");
                return ListaReservaciones;
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

        public static dynamic ObtenerReservacionPorId(int id)
        {
            try
            {
                Reservacion reservacion = null;

                using (conexion)
                {
                    conexion.abrirConexion();
                    Query = $"SELECT * FROM Reservaciones WHERE IdReservacion = {id}";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        reservacion = MapearReservacionDesdeDataReader(reader);
                    }
                }

                return reservacion;
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

        public static dynamic EliminarReservacion(int id)
        {
            try
            {
                using (conexion)
                {
                    conexion.abrirConexion();
                    Query = $"Update Reservaciones set Activo = 0 where IdReservacion = {id}";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.ExecuteNonQuery();
                }

                Console.WriteLine($"Se eliminó la reservación con Id {id}.");
                return Respuesta.getRespuesta("Se eliminó la reservación exitosamente.", "0000", "");
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

       

        public static string ObtenerTokenEnIdReservacion(int reservationId)
        {
            try
            {
                using (conexion)
                {
                    conexion.abrirConexion();
                    Query = $"SELECT Token FROM Token WHERE ID_Reservacion = {reservationId}";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        return reader.GetValue(0).ToString();
                    }
                }
            }
            catch
            {
                // Handle exceptions if needed
            }

            return null;
        }

        public static Reservacion MapearReservacionDesdeDataReader(SqlDataReader reader)
        {


            return new Reservacion
            {
                IdReservacion = reader.GetInt32(reader.GetOrdinal("IdReservacion")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                IdHabitacion = reader.GetInt32(reader.GetOrdinal("IdHabitacion")),
                FechaCheckIn = reader.GetDateTime(reader.GetOrdinal("FechaCheckIn")),
                FechaCheckOut = reader.GetDateTime(reader.GetOrdinal("FechaCheckOut")),
                HoraCheckIn = reader.IsDBNull(reader.GetOrdinal("HoraCheckIn")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("HoraCheckIn")),
                HoraCheckOut = reader.IsDBNull(reader.GetOrdinal("HoraCheckOut")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("HoraCheckOut")),
                MetodoPagoC1 = reader.IsDBNull(reader.GetOrdinal("MetodoPagoC1")) ? null : reader.GetString(reader.GetOrdinal("MetodoPagoC1")),
                ComentarioR = reader.IsDBNull(reader.GetOrdinal("ComentarioR")) ? null : reader.GetString(reader.GetOrdinal("ComentarioR")),
                CantidadHuesped = reader.IsDBNull(reader.GetOrdinal("CantidadHuesped")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantidadHuesped")),
                Activo=reader.IsDBNull(reader.GetOrdinal("Activo")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantidadHuesped"))
            };
        }





        //----------------------------------------------------------------------------------------------------------------


        public static int InsertarReservacion(Reservacion nuevaReservacion)
        {
            try
            {
                using (var conexion = new connectionsDB())
                {
                    conexion.abrirConexion();

                    // Utilizar una consulta INSERT directa
                    Query = "INSERT INTO Reservaciones (IdHabitacion, CantidadHuesped, IdCliente, FechaNow, FechaCheckIn, FechaCheckOut, HoraCheckIn, HoraCheckOut, PagoC1, MetodoPagoC1, Activo, ComentarioR) " +
                     "VALUES(@IdHabitacion, @CantidadHuesped, @IdCliente, GETDATE(), @FechaCheckIn, @FechaCheckOut, @HoraCheckIn, @HoraCheckOut, 1, @MetodoPagoC1, 1, @ComentarioR); " +
                     "SELECT SCOPE_IDENTITY();";

                    // Configurar el comando SQL con parámetros
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.Parameters.AddWithValue("@IdCliente", nuevaReservacion.IdCliente);
                    sqlCommand.Parameters.AddWithValue("@IdHabitacion", nuevaReservacion.IdHabitacion);
                    sqlCommand.Parameters.AddWithValue("@FechaCheckIn", nuevaReservacion.FechaCheckIn);
                    sqlCommand.Parameters.AddWithValue("@FechaCheckOut", nuevaReservacion.FechaCheckOut);
                    sqlCommand.Parameters.AddWithValue("@HoraCheckIn", nuevaReservacion.HoraCheckIn);
                    sqlCommand.Parameters.AddWithValue("@HoraCheckOut", nuevaReservacion.HoraCheckOut);
                    sqlCommand.Parameters.AddWithValue("@MetodoPagoC1", nuevaReservacion.MetodoPagoC1 );
                    sqlCommand.Parameters.AddWithValue("@ComentarioR", nuevaReservacion.ComentarioR);
                    sqlCommand.Parameters.AddWithValue("@CantidadHuesped", nuevaReservacion.CantidadHuesped);
                    sqlCommand.Parameters.AddWithValue("@Activo", nuevaReservacion.Activo);



                    // Ejecutar la consulta y obtener el ID de la reservación recién insertada
                    int idReservacion = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    conexion.cerrarConexion();

                    Console.WriteLine($"Se insertó una nueva reservación en la base de datos con ID {idReservacion}.");
                    return idReservacion;
                }
            }
            catch (SqlException Error)
            {
                Console.WriteLine($"Error al insertar la reservación: {Error}");
                throw; // Puedes manejar este error según sea necesario
            }
            catch (Exception Error)
            {
                Console.WriteLine($"Error general al insertar la reservación: {Error}");
                throw; // Puedes manejar este error según sea necesario
            }
        }
















        //actualizar reservacion
        public static Respuesta ActualizarReservacion(int id, Reservacion reservacion)
        {
            try
            {
                using (conexion)
                {
                    conexion.abrirConexion();

                    Query = $"EXEC UpdateReservacion " +
                            $"@IdReservacion = {reservacion.IdReservacion}, " +
                            $"@IdCliente = {reservacion.IdCliente}, " +
                            $"@IdHabitacion = {reservacion.IdHabitacion}, " +
                            $"@CantidadHuesped = {reservacion.CantidadHuesped}, " +
                            $"@FechaCheckIn = '{reservacion.FechaCheckIn:yyyy-MM-dd}', " +
                            $"@FechaCheckOut = '{reservacion.FechaCheckOut:yyyy-MM-dd}', " +
                            $"@HoraCheckIn = '{reservacion.HoraCheckIn:hh\\:mm\\:ss}', " +
                            $"@HoraCheckOut = '{reservacion.HoraCheckOut:hh\\:mm\\:ss}', " +
                            $"@Estado = '{reservacion.Estado}', " +
                            $"@EstadoPago = '{reservacion.EstadoPago}', " +
                            $"@MetodoPagoC1 = '{reservacion.MetodoPagoC1}', " +
                            $"@ComentarioR = '{reservacion.ComentarioR}';";

                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();

                    conexion.cerrarConexion();

                    Console.WriteLine($"Actualización de registro de la reservación con ID {reservacion.IdReservacion}.");
                    return Respuesta.getRespuesta("Se actualizó la reservación.", "0000", "");
                }
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






        public static Respuesta PagarCuota2(int idReservacion, string metodoPagoC2)
        {
            try
            {
                using (var conexion = new connectionsDB())
                {
                    conexion.abrirConexion();

                    // Utilizar una consulta UPDATE para realizar el pago de la segunda cuota
                    Query = "UPDATE Reservaciones SET PagoC2 = 1, MetodoPagoC2 = @MetodoPagoC2 WHERE IdReservacion = @Id";

                    // Configurar el comando SQL con parámetros
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.Parameters.AddWithValue("@MetodoPagoC2", metodoPagoC2);
                    sqlCommand.Parameters.AddWithValue("@Id", idReservacion);

                    // Ejecutar la consulta
                    sqlCommand.ExecuteNonQuery();

                    conexion.cerrarConexion();

                    Console.WriteLine($"Se realizó el pago de la segunda cuota para la reservación con ID {idReservacion}.");
                    return Respuesta.getRespuesta("Se realizó el pago de la segunda cuota.", "0000", "");
                }
            }
            catch (SqlException Error)
            {
                Console.WriteLine($"Error al realizar el pago de la segunda cuota: {Error}");
                return Respuesta.getRespuesta("Error al realizar el pago de la segunda cuota.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                Console.WriteLine($"Error general al realizar el pago de la segunda cuota: {Error}");
                return Respuesta.getRespuesta("Error general al realizar el pago de la segunda cuota.", "9990", $"{Error}");
            }
        }

        public static Respuesta RealizarCheckIn(int idReservacion)
        {
            try
            {
                using (var conexion = new connectionsDB())
                {
                    conexion.abrirConexion();

                    // Utilizar una consulta UPDATE para realizar el check-in
                    Query = "UPDATE Reservaciones SET EstadoCheckIn = 1 WHERE IdReservacion = @Id";

                    // Configurar el comando SQL con parámetros
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.Parameters.AddWithValue("@Id", idReservacion);

                    // Ejecutar la consulta
                    sqlCommand.ExecuteNonQuery();

                    conexion.cerrarConexion();

                    Console.WriteLine($"Se realizó el check-in para la reservación con ID {idReservacion}.");
                    return Respuesta.getRespuesta("Se realizó el check-in.", "0000", "");
                }
            }
            catch (SqlException Error)
            {
                Console.WriteLine($"Error al realizar el check-in: {Error}");
                return Respuesta.getRespuesta("Error al realizar el check-in.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                Console.WriteLine($"Error general al realizar el check-in: {Error}");
                return Respuesta.getRespuesta("Error general al realizar el check-in.", "9990", $"{Error}");
            }
        }

        public static Respuesta RealizarCheckOut(int idReservacion)
        {
            try
            {
                using (var conexion = new connectionsDB())
                {
                    conexion.abrirConexion();

                    // Utilizar una consulta UPDATE para realizar el check-out
                    Query = "UPDATE Reservaciones SET EstadoCheckOut = 1 WHERE IdReservacion = @Id";

                    // Configurar el comando SQL con parámetros
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.Parameters.AddWithValue("@Id", idReservacion);

                    // Ejecutar la consulta
                    sqlCommand.ExecuteNonQuery();

                    conexion.cerrarConexion();

                    Console.WriteLine($"Se realizó el check-out para la reservación con ID {idReservacion}.");
                    return Respuesta.getRespuesta("Se realizó el check-out.", "0000", "");
                }
            }
            catch (SqlException Error)
            {
                Console.WriteLine($"Error al realizar el check-out: {Error}");
                return Respuesta.getRespuesta("Error al realizar el check-out.", "9998", $"{Error}");
            }
            catch (Exception Error)
            {
                Console.WriteLine($"Error general al realizar el check-out: {Error}");
                return Respuesta.getRespuesta("Error general al realizar el check-out.", "9990", $"{Error}");
            }
        }








    }
}


  




