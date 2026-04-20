using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Encuesta
{
    public class ResponderEncuestaDTO
    {
        [Required]
        public int ENC_Encuesta { get; set; }

        public List<ResponderEncuestaCampoDTO> Campos { get; set; }

        public ResponderEncuestaDTO()
        {
            Campos = new List<ResponderEncuestaCampoDTO>();
        }
    }
}