using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.RespuestaDetalle
{
    public class ResponseRespuestaDetalleDTO
    {
        public int RED_Detalle { get; set; }
        public string RED_Codigo { get; set; }

        public int RES_Respuesta { get; set; }
        public string RES_Codigo { get; set; }

        public int CAM_Campo { get; set; }
        public string CAM_Titulo_Visible { get; set; }
        public string TCA_Descripcion { get; set; }

        public string RED_Valor_Texto { get; set; }
        public decimal? RED_Valor_Numero { get; set; }
        public DateTime? RED_Valor_Fecha { get; set; }

        public string OpcionesSeleccionadas { get; set; }
    }
}