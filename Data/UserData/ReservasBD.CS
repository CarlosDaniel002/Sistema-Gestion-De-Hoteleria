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
    public class ReservacionDB
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
                    Query = $"DELETE FROM Reservaciones WHERE IdReservacion = {id}";
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

        public static int? ObtenerUltimoIdReservacion()
        {
            try
            {
                using (conexion)
                {
                    conexion.abrirConexion();
                    Query = "SELECT IDENT_CURRENT('Reservaciones')";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        return Convert.ToInt32(reader.GetValue(0));
                    }
                }
            }
            catch
            {
                // Handle exceptions if needed
            }

            return null;
        }

        public static void AsignarTokenParaReservacion(int? reservationId, Guid token)
        {
            try
            {
                using (conexion)
                {
                    conexion.abrirConexion();
                    Query = $"INSERT INTO Token (ID_Reservacion, Token, FechaHora) VALUES ('{reservationId}', '{token}', GETDATE())";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                // Handle exceptions if needed
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
                CantidadHuesped = reader.IsDBNull(reader.GetOrdinal("CantidadHuesped")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantidadHuesped"))
            };
        }






        public static dynamic InsertarReservacion(Reservacion reservation)
        {
            try
            {
                if (reservation.IdReservacion == 0)
                {
                    using (conexion)
                    {
                        conexion.abrirConexion();

                        Query = "INSERT INTO Reservaciones (IdCliente, IdHabitacion, CantidadHuesped, FechaCheckIn, FechaCheckOut, HoraCheckIn, HoraCheckOut, MetodoPagoC1, ComentarioR) " +
                                "VALUES (@IdCliente, @IdHabitacion, @CantidadHuesped, @FechaCheckIn, @FechaCheckOut, @HoraCheckIn, @HoraCheckOut, @MetodoPagoC1, @ComentarioR);";

                        sqlCommand = new SqlCommand(Query, conexion.dataBase);
                        sqlCommand.Parameters.AddWithValue("@IdCliente", reservation.IdCliente);
                        sqlCommand.Parameters.AddWithValue("@IdHabitacion", reservation.IdHabitacion);
                        sqlCommand.Parameters.AddWithValue("@CantidadHuesped", reservation.CantidadHuesped);
                        sqlCommand.Parameters.AddWithValue("@FechaCheckIn", reservation.FechaCheckIn);
                        sqlCommand.Parameters.AddWithValue("@FechaCheckOut", reservation.FechaCheckOut);
                        sqlCommand.Parameters.AddWithValue("@HoraCheckIn", reservation.HoraCheckIn);
                        sqlCommand.Parameters.AddWithValue("@HoraCheckOut", reservation.HoraCheckOut);

                        sqlCommand.Parameters.AddWithValue("@MetodoPagoC1", reservation.MetodoPagoC1 ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@ComentarioR", reservation.ComentarioR ?? (object)DBNull.Value);

                        sqlCommand.ExecuteNonQuery();
                    }

                    var reservationId = ObtenerUltimoIdReservacion();
                    if (reservationId != null)
                    {
                        var token = Guid.NewGuid();
                        AsignarTokenParaReservacion(reservationId, token);
                    }

                    return reservationId ?? 0;
                }

                return Respuesta.getRespuesta("El ID de la reservación debe ser 0 para realizar una inserción.", "0000", "");
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


    }
}


