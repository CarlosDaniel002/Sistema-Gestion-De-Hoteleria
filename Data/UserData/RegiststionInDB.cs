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
                Query = $"EXEC InsertUsuario '{usuario.NombreCompleto}', '{usuario.NombreUsuario}', '{usuario.Contraseña}','{usuario.Salting}', '{usuario.RolUsuario}'";
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
    }
}
