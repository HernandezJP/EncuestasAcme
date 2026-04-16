using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Models
{
    public class ACE_OPCION_CAMPO
    {
        public int OPC_Opcion { get; set; }
        public string OPC_Codigo { get; set; }
        public int CAM_Campo { get; set; }
        public string OPC_Texto { get; set; }
        public string OPC_Valor { get; set; }
        public int OPC_Orden { get; set; }
        public string OPC_Estado { get; set; }
        public DateTime OPC_Fecha_Creacion { get; set; }

        public virtual ACE_CAMPO_ENCUESTA CampoEncuesta { get; set; }
    }
}