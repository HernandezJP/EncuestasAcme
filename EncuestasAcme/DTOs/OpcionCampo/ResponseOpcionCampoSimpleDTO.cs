using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.OpcionCampo
{
    public class ResponderOpcionCampoSimpleDTO
    {
        public int OPC_Opcion { get; set; }
        public string OPC_Texto { get; set; }
        public string OPC_Valor { get; set; }
        public int OPC_Orden { get; set; }
    }
}