using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.CampoEncuesta
{
    public class CreateCampoEncuestaDTO
    {
        [Required(ErrorMessage = "La encuesta es obligatoria.")]
        public int ENC_Encuesta { get; set; }

        [Required(ErrorMessage = "El tipo de campo es obligatorio.")]
        [Display(Name = "Tipo de campo")]
        public int TCA_Tipo_Campo { get; set; }

        [Required(ErrorMessage = "El nombre interno es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre interno no puede exceder 100 caracteres.")]
        [Display(Name = "Nombre interno")]
        public string CAM_Nombre_Interno { get; set; }

        [Required(ErrorMessage = "El título visible es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título visible no puede exceder 150 caracteres.")]
        [Display(Name = "Título visible")]
        public string CAM_Titulo_Visible { get; set; }

        [Display(Name = "Es requerido")]
        public bool CAM_Es_Requerido { get; set; }

        [Required(ErrorMessage = "El orden es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El orden debe ser mayor a 0.")]
        [Display(Name = "Orden")]
        public int CAM_Orden { get; set; }
    }
}