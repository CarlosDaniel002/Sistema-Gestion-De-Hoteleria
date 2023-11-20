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
    public class ClientesDB
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand sqlCommand;
        private static SqlDataReader reader;
        private static int commandTimeoutInSeconds = 30;
        private static string Query;

        public static dynamic GetAllClientes()
        {
            try
            {

                List<Cliente> ListaCliente = new List<Cliente>();
                Task.Run(() =>
                {
                    conexion.abrirConexion();
                }).Wait(TimeSpan.FromSeconds(30));


                Query = $"Select * from Clientes;";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                sqlCommand.CommandTimeout = commandTimeoutInSeconds;
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(5)))
                    {
                        ListaCliente.Add(new Cliente()
                        {
                            IdCliente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Correo = reader.GetString(4),
                            Activo = reader.GetByte(5)
                        });
                    }
                }
                conexion.cerrarConexion();
                Console.WriteLine($"Se cargo una lista de {ListaCliente.Count} - Clientes.");
                return ListaCliente;
            }
            catch (SqlException Error)
            {
                return Respuesta.getRespuesta("Fallo al conectarse a la DB.",$"{Error.Number}", $"{Error.Errors}");
            }
            catch (Exception Error)
            {
                return Respuesta.getRespuesta("Error general.", $"{Error.HResult}", $"{Error.Message} ");
            }
        }

        public static dynamic GetClienteAtId(int? Id)
        {
            try
            {
                Cliente ClienteID = null;
                conexion.abrirConexion();
                Query = $"Select * from Clientes where IdCliente = {Id};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(5)))
                    {
                        ClienteID = new Cliente()
                        {
                            IdCliente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Correo = reader.GetString(4),
                            Activo = reader.GetByte(5)
                        };
                    }
                }
                conexion.cerrarConexion();
                return ClienteID;
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


        public static dynamic SetCliente(Cliente cliente)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"EXEC InsertCliente '{cliente.Nombre}', '{cliente.Apellido}', '{cliente.Telefono}','{cliente.Correo}'";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Nuevo registro de {cliente.Nombre} - Cliente.");
                return Respuesta.getRespuesta("Se creo con exito el cliente.", "0000", "");
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
        
        public static Respuesta UpdateCliente(Cliente cliente)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"EXEC UpdateCliente {cliente.IdCliente},'{cliente.Nombre}', '{cliente.Apellido}', '{cliente.Telefono}','{cliente.Correo}';";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Actualización de registro de {cliente.Nombre} - Cliente.");
                return Respuesta.getRespuesta("Se actualizo el cliente.", "0000", "");
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

        public static dynamic DeleteCliente(int Id, int activar)
        {
            try
            {       conexion.abrirConexion();
                    Query = $"Update Clientes set Activo = {activar} where IdCliente = {Id};";
                    sqlCommand = new SqlCommand(Query, conexion.dataBase);
                    reader = sqlCommand.ExecuteReader();
                    conexion.cerrarConexion();
                    Console.WriteLine($"Se elimino el id {Id} - Cliente.");
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
