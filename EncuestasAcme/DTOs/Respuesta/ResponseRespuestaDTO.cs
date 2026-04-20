using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Respuesta
{
    public class ResponseRespuestaDTO
    {
        public int RES_Respuesta { get; set; }
        public string RES_Codigo { get; set; }
        public int ENC_Encuesta { get; set; }
        public string ENC_Nombre { get; set; }
        public DateTime RES_Fecha { get; set; }
        public string RES_IP { get; set; }
    }
}