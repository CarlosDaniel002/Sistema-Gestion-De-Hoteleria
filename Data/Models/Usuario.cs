﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio.")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Usuario es obligatorio.")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Contraseña es obligatorio.")]
        public string Contraseña { get; set; }
        public string Salting { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string RolUsuario { get; set; }
        public int Activo { get; set; }
        public int IdUsuario { get; internal set; }
        public object ContrasenaHash { get; internal set; }

        // Metodo para obtener el hash crudo, con el salting, ya que ambos son string en la BD
    }
}
