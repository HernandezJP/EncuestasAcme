using EncuestasAcme.DTOs.CampoEncuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Encuesta
{
    public class ResponseEncuestaDetalleDTO
    {
        public int ENC_Encuesta { get; set; }
        public string ENC_Codigo { get; set; }
        public string ENC_Nombre { get; set; }
        public string ENC_Descripcion { get; set; }
        public Guid ENC_Token_Publico { get; set; }
        public string ENC_Estado { get; set; }
        public DateTime ENC_Fecha_Creacion { get; set; }

        public List<ResponseCampoEncuestaDTO> Campos { get; set; }

        public ResponseEncuestaDetalleDTO()
        {
            Campos = new List<ResponseCampoEncuestaDTO>();
        }
    }
}