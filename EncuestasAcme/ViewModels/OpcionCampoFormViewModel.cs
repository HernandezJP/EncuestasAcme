using EncuestasAcme.DTOs.OpcionCampo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.ViewModels
{
    public class OpcionCampoFormViewModel
    {
        public int CAM_Campo { get; set; }
        public string CAM_Titulo_Visible { get; set; }
        public string TCA_Descripcion { get; set; }

        public CreateOpcionCampoDTO CreateDTO { get; set; }
        public UpdateOpcionCampoDTO UpdateDTO { get; set; }

        public OpcionCampoFormViewModel()
        {
            CreateDTO = new CreateOpcionCampoDTO();
            UpdateDTO = new UpdateOpcionCampoDTO();
        }
    }
}