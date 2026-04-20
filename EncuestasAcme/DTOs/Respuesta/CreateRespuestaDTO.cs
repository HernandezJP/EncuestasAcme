using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Respuesta
{
    public class CreateRespuestaDTO
    {
        [Required(ErrorMessage = "La encuesta es obligatoria.")]
        [Display(Name = "Encuesta")]
        public int ENC_Encuesta { get; set; }

        public string RES_IP { get; set; }
        public string RES_User_Agent { get; set; }
    }
}