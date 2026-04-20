using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.ViewModels
{
    public class DashboardFormViewModel
    {
        public int TotalEncuestas { get; set; }
        public int TotalCampos { get; set; }
        public int TotalRespuestas { get; set; }

        public string UltimaEncuestaNombre { get; set; }
        public string UltimaEncuestaFecha { get; set; }

        public List<DashboardRespuestaItemViewModel> UltimasRespuestas { get; set; }
        public List<DashboardEncuestaTopViewModel> EncuestasMasRespondidas { get; set; }

        public DashboardFormViewModel()
        {
            UltimasRespuestas = new List<DashboardRespuestaItemViewModel>();
            EncuestasMasRespondidas = new List<DashboardEncuestaTopViewModel>();
        }
    }

    public class DashboardRespuestaItemViewModel
    {
        public string Codigo { get; set; }
        public string Encuesta { get; set; }
        public string Fecha { get; set; }
    }

    public class DashboardEncuestaTopViewModel
    {
        public string Encuesta { get; set; }
        public int TotalRespuestas { get; set; }
    }
}
