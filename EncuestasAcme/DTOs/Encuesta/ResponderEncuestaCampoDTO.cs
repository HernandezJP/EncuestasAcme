using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Encuesta
{
    public class ResponderEncuestaCampoDTO
    {
        public int CAM_Campo { get; set; }
        public string TCA_Clave { get; set; }

        public string ValorTexto { get; set; }
        public decimal? ValorNumero { get; set; }
        public string ValorFecha { get; set; }

        public int? OpcionSeleccionada { get; set; }
        public List<int> OpcionesSeleccionadas { get; set; }

        public ResponderEncuestaCampoDTO()
        {
            OpcionesSeleccionadas = new List<int>();
        }
    }
}