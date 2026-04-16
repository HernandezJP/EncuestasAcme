using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Encuesta
{
    public class CreateEncuestaDTO
    {
        [Required(ErrorMessage = "El nombre de la encuesta es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre de la encuesta no puede exceder los 150 caracteres.")]
        public string ENC_Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción de la encuesta no puede exceder los 500 caracteres.")]
        public string ENC_Descripcion { get; set; }
    }
}