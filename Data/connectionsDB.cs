using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class connectionsDB
    {
        // Esto es una practica pero es algo temporal a modo de avance
        private string urlConexion = "Data Source=SQL8002.site4now.net; initial Catalog=db_aa062b_pfg10itla; user ID=db_aa062b_pfg10itla_admin;Password=PFG10ITLA";
        // private string urlConexion = "workstation id=PFG10ITLA.mssql.somee.com;packet size=4096;user id=pg10@domercd;pwd=5NR46+tcE!?k663072711;data source=PFG10ITLA.mssql.somee.com;persist security info=False;initial catalog=PFG10ITLA";

        internal SqlConnection dataBase = new SqlConnection();

        public connectionsDB()
        {
            dataBase.ConnectionString = urlConexion;
        }

        public void abrirConexion()
        {
            try
            {
                dataBase.Open();
                Console.WriteLine("Conexión abierta exito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se descubrieron diferentes errores: " + ex.Message);
            }
        }

        public void cerrarConexion()
        {
            dataBase.Close();
            Console.WriteLine("Conexión cerrada exito.");
        }
    }
}
