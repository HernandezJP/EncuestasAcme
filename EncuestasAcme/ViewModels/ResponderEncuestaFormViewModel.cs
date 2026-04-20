using EncuestasAcme.DTOs.Encuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.ViewModels
{
    public class ResponderEncuestaFormViewModel
    {
        public DetailEncuestaDTO Encuesta { get; set; }
        public ResponderEncuestaDTO Respuesta { get; set; }

        public ResponderEncuestaFormViewModel()
        {
            Encuesta = new DetailEncuestaDTO();
            Respuesta = new ResponderEncuestaDTO();
        }
    }
}