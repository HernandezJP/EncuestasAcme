using EncuestasAcme.DTOs.CampoEncuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.ViewModels
{
    public class CampoEncuestaFormViewModel
    {
        public CreateCampoEncuestaDTO CrearDTO { get; set; }
        public UpdateCampoEncuestaDTO ActualizarDTO { get; set; }

        public List<SelectListItem> TiposCampo { get; set; }

        public int ENC_Encuesta { get; set; }
        public string ENC_Nombre { get; set; }

        public CampoEncuestaFormViewModel()
        {
            TiposCampo = new List<SelectListItem>();
            CrearDTO = new CreateCampoEncuestaDTO();
            ActualizarDTO = new UpdateCampoEncuestaDTO();
        }
    }
}