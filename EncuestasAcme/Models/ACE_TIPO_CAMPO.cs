using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Models
{
    public class ACE_TIPO_CAMPO
    {
        public int TCA_Tipo_Campo { get; set; }
        public string TCA_Codigo { get; set; }
        public string TCA_Clave { get; set; }
        public string TCA_Descripcion { get; set; }
        public bool TCA_Permite_Opciones { get; set; }
        public bool TCA_Permite_Multiples { get; set; }
        public string TCA_Estado { get; set; }
        public DateTime TCA_Fecha_Creacion { get; set; }
    }
}