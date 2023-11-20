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
        private string urlConexion = "Data Source=SQL8002.site4now.net; initial Catalog=db_aa062b_pfg10itla; user ID=db_aa062b_pfg10itla_admin;Password=PFG10ITLA";

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
