using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;

namespace Data.UserData
{
    public class RegiststionInDB
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand? sqlCommand;
        private static SqlDataReader? reader;
        private static string? Query;

        public static dynamic SetUser(Usuario usuario, Guid Token)
        {
            int? ID = null;
            DateTime fechaHoraActual = DateTime.Now;
            if (usuario.IdUsuario == 0)
            {
                conexion.abrirConexion();
                Query = $"EXEC InsertUsuario '{usuario.NombreCompleto}', '{usuario.NombreUsuario}', '{usuario.ContrasenaHash}','{usuario.Salting}', '{usuario.RolUsuario}'";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();
                ID = GetIdUser();
            }
            if (ID != null)
            {
                SetTokenForUser(ID, Token);
            }
            var Respuesta = new
            {
                ID = ID,
                Token = Token
            };
            return Respuesta;
        }

        public static int? GetIdUser()
        {
            int? ID = null;
            conexion.abrirConexion();
            Query = $"EXEC ObtenerUltimoID;";
            sqlCommand = new SqlCommand(Query, conexion.dataBase);
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ID = int.Parse(reader.GetValue(0).ToString());
            }
            conexion.cerrarConexion();
            return ID;
        }

        public static void SetTokenForUser(int? ID, Guid Token)
        {
            var fechaHoraActual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            conexion.abrirConexion();
            Query = $"Insert Token values ('{ID}','{Token}','{fechaHoraActual}')";
            sqlCommand = new SqlCommand(Query, conexion.dataBase);
            reader = sqlCommand.ExecuteReader();
            conexion.cerrarConexion();
        }

        public static void SetUpdateTokenForUser(int? ID, Guid Token)
        {
            var fechaHoraActual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            conexion.abrirConexion();
            Query = $"EXEC ActualizarPorIdToken '{Token}','{ID}';";
            sqlCommand = new SqlCommand(Query, conexion.dataBase);
            reader = sqlCommand.ExecuteReader();
            conexion.cerrarConexion();
        }

        public static string? GetTokenAtIdUser(int ID) {
            string? Token = null;
            conexion.abrirConexion();
            Query = $"Select Token from Token where ID_Usuario = {ID};";
            sqlCommand = new SqlCommand(Query, conexion.dataBase);
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Token = reader.GetValue(0).ToString();
            }
            conexion.cerrarConexion();
            return Token;
        }




        public static dynamic ActualizarContrasena(int idUsuario, string nuevaContrasena, string nuevoSalting, Guid nuevoToken)
        {
            try
            {
                conexion.abrirConexion();

                // Actualizar la contraseña, salting y el token del usuario
                Query = $"EXEC ActualizarContrasenaSalting '{nuevaContrasena}', '{nuevoSalting}', '{nuevoToken}', {idUsuario};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                sqlCommand.ExecuteNonQuery();

                // Actualizar el token en la tabla Token
                SetUpdateTokenForUser(idUsuario, nuevoToken);

                conexion.cerrarConexion();

                return Respuesta.getRespuesta("Contraseña y salting actualizados con éxito.", "0000", "");
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





    }
}
