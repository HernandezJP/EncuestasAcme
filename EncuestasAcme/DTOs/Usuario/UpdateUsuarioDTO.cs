using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Usuario
{
    public class UpdateUsuarioDTO
    {
        [Required]
        public int USU_Usuario { get; set; }

        [Required(ErrorMessage = "El username es obligatorio.")]
        [StringLength(50)]
        public string USU_User_Name { get; set; }

        [StringLength(255)]
        public string USU_Password_Hash { get; set; }

        [Required(ErrorMessage = "El primer nombre es obligatorio.")]
        [StringLength(25)]
        public string USU_Primer_Nombre { get; set; }

        [StringLength(25)]
        public string USU_Segundo_Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [StringLength(25)]
        public string USU_Primer_Apellido { get; set; }

        [StringLength(25)]
        public string USU_Segundo_Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        [StringLength(150)]
        public string USU_Correo_Electronico { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int ROL_Rol { get; set; }
    }
}