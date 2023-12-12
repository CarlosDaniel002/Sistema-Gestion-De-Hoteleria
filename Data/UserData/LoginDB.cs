using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Dapper;

namespace Data.UserData
{
    public class LoginDB
    {
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
    }
}
