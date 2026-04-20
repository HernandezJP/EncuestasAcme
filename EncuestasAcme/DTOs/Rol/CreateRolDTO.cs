using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Rol
{
    public class CreateRolDTO
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres.")]
        public string ROL_Nombre { get; set; }

        [StringLength(150, ErrorMessage = "La descripción no puede exceder 150 caracteres.")]
        public string ROL_Descripcion { get; set; }
    }
}