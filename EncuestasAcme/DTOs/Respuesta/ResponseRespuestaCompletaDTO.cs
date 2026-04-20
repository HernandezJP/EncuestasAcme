using EncuestasAcme.DTOs.RespuestaDetalle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Respuesta
{
    public class ResponseRespuestaCompletaDTO
    {
        public int RES_Respuesta { get; set; }
        public string RES_Codigo { get; set; }
        public int ENC_Encuesta { get; set; }
        public string ENC_Nombre { get; set; }
        public DateTime RES_Fecha { get; set; }
        public string RES_IP { get; set; }
        public string RES_User_Agent { get; set; }

        public List<ResponseRespuestaDetalleDTO> Detalles { get; set; }

        public ResponseRespuestaCompletaDTO()
        {
            Detalles = new List<ResponseRespuestaDetalleDTO>();
        }


    }
}