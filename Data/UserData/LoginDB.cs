using Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class LoginDB
    {
        private static string connectionString = "Cadena de conexion";

        public static Usuario GetUserByUserName(string nombreUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                var parameters = new { NombreUsuario = nombreUsuario };
                return connection.QueryFirstOrDefault<Usuario>(query, parameters);
            }
        }

        public static string GetSaltingForUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT Salting FROM Usuarios WHERE IdUsuario = @UserId";
                var parameters = new { UserId = userId };
                return connection.QueryFirstOrDefault<string>(query, parameters);
            }
        }
    }
}


