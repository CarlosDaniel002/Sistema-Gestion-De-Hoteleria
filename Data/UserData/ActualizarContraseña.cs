using Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class ActualizarContraseña
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand sqlCommand;
        private static SqlDataReader reader;
        private static int commandTimeoutInSeconds = 30;
        private static string Query;

        public static dynamic ActualizarContrasena(int idUsuario, string nombreUsuario, string nuevoHashContraseña, string confirmarContrasena, Guid nuevoToken)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(conexion.dataBase.ConnectionString))
                {
                    connection.Open();

                    // Actualizar la contraseña
                    using (SqlCommand sqlCommandContraseña = new SqlCommand("dbo.ActualizarContrasena", connection))
                    {
                        sqlCommandContraseña.CommandType = CommandType.StoredProcedure;

                        sqlCommandContraseña.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        sqlCommandContraseña.Parameters.AddWithValue("@ContrasenaHash", nuevoHashContraseña);

                        sqlCommandContraseña.ExecuteNonQuery();
                    }

                    // Actualizar el token
                    using (SqlCommand sqlCommandToken = new SqlCommand("dbo.ActualizarToken", connection))
                    {
                        sqlCommandToken.CommandType = CommandType.StoredProcedure;

                        sqlCommandToken.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        sqlCommandToken.Parameters.AddWithValue("@NuevoToken", nuevoToken);

                        sqlCommandToken.ExecuteNonQuery();
                    }
                }

                // Resto de tu código después de la actualización
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
    

        public static void SetUpdateTokenForUser(int? ID, Guid Token, string nuevoHashContraseña)
        {
            var fechaHoraActual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            conexion.abrirConexion();
            Query = $"EXEC ActualizarPorIdToken '{Token}','{ID}';";
            sqlCommand = new SqlCommand(Query, conexion.dataBase);
            reader = sqlCommand.ExecuteReader();
            conexion.cerrarConexion();
        }

        public static string ObtenerSaltingActual(int idUsuario)
        {
            // Obtener el salting actual del usuario desde la base de datos
            string saltingActual = string.Empty;

            using (SqlConnection connection = new SqlConnection(conexion.dataBase.ConnectionString))
            {
                connection.Open();

                // Inicializa la variable Query antes de usarla en el SqlCommand
                string query = $"SELECT Salting FROM Usuarios WHERE IdUsuario = {idUsuario};";

                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            if (reader["Salting"] != DBNull.Value)
                            {
                                saltingActual = reader["Salting"].ToString();
                            }
                            else
                            {
                                // Manejar el caso de valor nulo según tus necesidades
                                saltingActual = string.Empty;
                            }
                        }
                    }
                }
            }
            return saltingActual;
        }




        //getpasswork
        public static byte[] StringToByteArray(string dato)
        {
            if (dato.Length % 2 != 0)
            {
                // La longitud de la cadena debe ser par
                dato = "0" + dato;
            }

            int numberChars = dato.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(dato.Substring(i, 2), 16);
            }
            return bytes;
        }






    }




    //Clases copiadas. me daba errror llamar la clase y su metodo de la clase getpassword


}
