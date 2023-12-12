using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class connectionsDB : IDisposable
    {
        private string urlConexion = "Data Source=SQL8002.site4now.net; initial Catalog=db_aa062b_pfg10itla; user ID=db_aa062b_pfg10itla_admin;Password=PFG10ITLA";

        internal SqlConnection dataBase = new SqlConnection();
        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~connectionsDB()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
