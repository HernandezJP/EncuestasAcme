using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Models
{
    public class ACE_RESPUESTA_DETALLE
    {
        public int RED_Detalle { get; set; }
        public string RED_Codigo { get; set; }
        public int RES_Respuesta { get; set; }
        public int CAM_Campo { get; set; }
        public string RED_Valor_Texto { get; set; }
        public decimal? RED_Valor_Numero { get; set; }
        public DateTime? RED_Valor_Fecha { get; set; }
    }
}