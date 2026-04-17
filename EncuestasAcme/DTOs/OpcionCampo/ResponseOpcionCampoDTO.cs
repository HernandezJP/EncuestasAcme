using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.OpcionCampo
{
    public class ResponseOpcionCampoDTO
    {
        public int OPC_Opcion { get; set; }
        public string OPC_Codigo { get; set; }

        public int CAM_Campo { get; set; }
        public string CAM_Titulo_Visible { get; set; }

        public string OPC_Texto { get; set; }
        public string OPC_Valor { get; set; }

        public int OPC_Orden { get; set; }
        public string OPC_Estado { get; set; }
    }
}