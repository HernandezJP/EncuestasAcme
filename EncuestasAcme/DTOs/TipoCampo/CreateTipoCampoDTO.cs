using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.TipoCampo
{
    public class CreateTipoCampoDTO
    {
        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(30, ErrorMessage = "La clave no puede exceder 30 caracteres.")]
        public string TCA_Clave { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres.")]
        public string TCA_Descripcion { get; set; }

        public bool TCA_Permite_Opciones { get; set; }
        public bool TCA_Permite_Multiples { get; set; }
    }
}