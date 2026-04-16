using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Models
{
    public class ACE_ENCUESTA
    {
        public int ENC_Encuesta { get; set; }
        public string ENC_Codigo { get; set; }
        public int USU_Usuario { get; set; }
        public string ENC_Nombre { get; set; }
        public string ENC_Descripcion { get; set; }
        public Guid ENC_Token_Publico { get; set; }
        public string ENC_Estado { get; set; }
        public DateTime ENC_Fecha_Creacion { get; set; }
        public DateTime? ENC_Fecha_Modificacion { get; set; }
    }
}