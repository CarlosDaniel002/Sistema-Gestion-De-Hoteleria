using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Globalization;
using System.Collections;
using System.Reflection.PortableExecutable;

namespace Data.UserData
{
    public class LoginDB
    {
        private static SqlCommand? sqlCommand;
        private static SqlDataReader? reader;
        private static string? Query;
        private connectionsDB connectionDB = new connectionsDB();

        public Usuario GetUserByUserName(string nombreUsuario)
        {
            using (var connection = new SqlConnection(connectionDB.dataBase.ConnectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                var parameters = new { NombreUsuario = nombreUsuario };
                return connection.QueryFirstOrDefault<Usuario>(query, parameters);
            }
        }

        public string GetSaltingForUser(int userId)
        {
            using (var connection = new SqlConnection(connectionDB.dataBase.ConnectionString))
            {
                connection.Open();
                var query = "SELECT Salting FROM Usuarios WHERE IdUsuario = @UserId";
                var parameters = new { UserId = userId };
                return connection.QueryFirstOrDefault<string>(query, parameters);
            }
        }

        private Usuario GetUserByUserId(int id)
        {
            using (var connection = new SqlConnection(connectionDB.dataBase.ConnectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Usuarios WHERE IdUsuario = @Id";
                var parameters = new { Id = id };
                return connection.QueryFirstOrDefault<Usuario>(query, parameters);
            }
        }
        public Usuario GetUserByUserToken (string token)
        {
            Usuario usuario = null;
            string id = null;

            using (var connection = new SqlConnection(connectionDB.dataBase.ConnectionString))
            {
                connection.Open();
                var query = "SELECT ID_Usuario FROM Token WHERE Token = @Token";
                var parameters = new { Token = token };
                id = connection.QueryFirstOrDefault<string>(query, parameters);
                connection.QueryFirstOrDefault(query, parameters);
            }
            
            return GetUserByUserId( Convert.ToInt32(id));
        }

        public void SetNewPasswordByIdUser(int Id, string clave)
        {
            using (var connection = new SqlConnection(connectionDB.dataBase.ConnectionString))
            {
                connection.Open();
                var query = "EXEC ActualizarClave @Nuevo = @Clave, @ID = @IDU; ";
                var parameters = new { IDU = Id, Clave = clave };
                connection.QueryFirstOrDefault(query, parameters);
            }
        }
    }
}
