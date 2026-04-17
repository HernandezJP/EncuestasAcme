using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.CampoEncuesta
{
    public class ResponseCampoEncuestaDTO
    {
        public int CAM_Campo { get; set; }
        public string CAM_Codigo { get; set; }

        public int ENC_Encuesta { get; set; }
        public string ENC_Nombre { get; set; }
        public int TCA_Tipo_Campo { get; set; }
        public string TCA_Descripcion { get; set; }
        public bool TCA_Permite_Opciones { get; set; }
        public bool TCA_Permite_Multiples { get; set; }
        public string CAM_Nombre_Interno { get; set; }
        public string CAM_Titulo_Visible { get; set; }

        public bool CAM_Es_Requerido { get; set; }
        public int CAM_Orden { get; set; }

        public string CAM_Estado { get; set; }
    }
}