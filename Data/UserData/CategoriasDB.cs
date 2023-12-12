using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UserData
{
    public class CategoriasDB
    {
        private static connectionsDB conexion = new connectionsDB();
        private static SqlCommand sqlCommand;
        private static SqlDataReader reader;
        private static int commandTimeoutInSeconds = 30;
        private static string Query;

        public static dynamic GetAllCategorias()
        {
            try
            {
                List<Categoria> ListaCategorias = new List<Categoria>();
                Task.Run(() => {
                    conexion.abrirConexion();
                }).Wait(TimeSpan.FromSeconds(30));

                Query = $"SELECT * FROM Categorias;";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                sqlCommand.CommandTimeout = commandTimeoutInSeconds;
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(7)))
                    {
                        ListaCategorias.Add(new Categoria()
                        {
                            IdCategoria = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            CantidadMax = reader.GetInt32(2),
                            Precio = reader.GetDecimal(3),
                            Cuota1 = reader.IsDBNull(4) ? null : (decimal?)reader.GetDecimal(4),
                            Cuota2 = reader.IsDBNull(5) ? null : (decimal?)reader.GetDecimal(5),
                            ComentarioC = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Activo = reader.GetByte(7)
                        });
                    }
                }

                conexion.cerrarConexion();
                Console.WriteLine($"Se cargó una lista de {ListaCategorias.Count} - Categorias.");
                return ListaCategorias;
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

        public static dynamic GetCategoriaAtId(int? Id)
        {
            try
            {
                Categoria CategoriaID = null;
                conexion.abrirConexion();
                Query = $"SELECT * FROM Categorias WHERE IdCategoria = {Id};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader.GetByte(7)))
                    {
                        CategoriaID = new Categoria()
                        {
                            IdCategoria = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            CantidadMax = reader.GetInt32(2),
                            Precio = reader.GetDecimal(3),
                            Cuota1 = reader.IsDBNull(4) ? null : (decimal?)reader.GetDecimal(4),
                            Cuota2 = reader.IsDBNull(5) ? null : (decimal?)reader.GetDecimal(5),
                            ComentarioC = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Activo = reader.GetByte(7)
                        };
                    }
                }

                conexion.cerrarConexion();
                return CategoriaID;
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

        public static dynamic SetCategoria(Categoria categoria)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"EXEC InsertCategoria '{categoria.Nombre}', {categoria.CantidadMax}, {categoria.Precio}, '{categoria.ComentarioC}'";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Nuevo registro de {categoria.Nombre} - Categoria.");
                return Respuesta.getRespuesta("Se creó con éxito la categoría.", "0000", "");
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

        public static Respuesta UpdateCategoria(Categoria categoria)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"EXEC UpdateCategoria {categoria.IdCategoria}, '{categoria.Nombre}', {categoria.CantidadMax}, {categoria.Precio}, {categoria.Cuota1}, {categoria.Cuota2}, '{categoria.ComentarioC}'";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Actualización de registro de {categoria.Nombre} - Categoria.");
                return Respuesta.getRespuesta("Se actualizó la categoría.", "0000", "");
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

        public static dynamic DeleteCategoria(int Id, int activar)
        {
            try
            {
                conexion.abrirConexion();
                Query = $"UPDATE Categorias SET Activo = {activar} WHERE IdCategoria = {Id};";
                sqlCommand = new SqlCommand(Query, conexion.dataBase);
                reader = sqlCommand.ExecuteReader();
                conexion.cerrarConexion();

                Console.WriteLine($"Se eliminó el ID {Id} - Categoria.");
                return activar == 0 ? Respuesta.getRespuesta("Se eliminó exitosamente.", "0000", "") : Respuesta.getRespuesta("Se activó exitosamente.", "0000", "");
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
