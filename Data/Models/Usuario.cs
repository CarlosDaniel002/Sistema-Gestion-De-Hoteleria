using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Usuario
    {
        // Datos de usuario
        /*
        CREATE TABLE Usuarios(
    IdUsuario INT PRIMARY KEY IDENTITY,
    NombreCompleto VARCHAR(150) NOT NULL,
    NombreUsuario   VARCHAR(15) NOT NULL,
    Contrase�aHash VARCHAR(15) NOT NULL,
    Salting         VARCHAR(15),
        RolUsuario VARCHAR(15) NOT NULL,
        Activo          TINYINT NOT NULL
    );
    GO*/

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Salting { get; set; }
        public string RolUsuario { get; set; }
        public int Activo { get; set; }

        // Metodo para obtener el hash crudo, con el salting, ya que ambos son string en la BD
    }
}
