using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.OpcionCampo
{
    public class UpdateOpcionCampoDTO
    {
        [Required]
        public int OPC_Opcion { get; set; }

        [Required(ErrorMessage = "El texto de la opción es obligatorio.")]
        [StringLength(200, ErrorMessage = "El texto no puede exceder 200 caracteres.")]
        [Display(Name = "Texto")]
        public string OPC_Texto { get; set; }

        [Required(ErrorMessage = "El valor de la opción es obligatorio.")]
        [StringLength(100, ErrorMessage = "El valor no puede exceder 100 caracteres.")]
        [Display(Name = "Valor")]
        public string OPC_Valor { get; set; }

        [Required(ErrorMessage = "El orden es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El orden debe ser mayor a 0.")]
        [Display(Name = "Orden")]
        public int OPC_Orden { get; set; }
    }
}