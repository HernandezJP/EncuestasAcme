using EncuestasAcme.Data;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext db;

        public DashboardService()
        {
            db = new ApplicationDbContext();
        }

        public DashboardFormViewModel ObtenerDashboard()
        {
            var vm = new DashboardFormViewModel();

            vm.TotalEncuestas = db.Encuestas.Count(x => x.ENC_Estado == "A");
            vm.TotalCampos = db.CamposEncuesta.Count(x => x.CAM_Estado == "A");
            vm.TotalRespuestas = db.Respuestas.Count();

            var ultimaEncuesta = db.Encuestas
                .Where(x => x.ENC_Estado == "A")
                .OrderByDescending(x => x.ENC_Fecha_Creacion)
                .FirstOrDefault();

            if (ultimaEncuesta != null)
            {
                vm.UltimaEncuestaNombre = ultimaEncuesta.ENC_Nombre;
                vm.UltimaEncuestaFecha = ultimaEncuesta.ENC_Fecha_Creacion.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                vm.UltimaEncuestaNombre = "Sin registros";
                vm.UltimaEncuestaFecha = "-";
            }

            vm.UltimasRespuestas = db.Respuestas
                .OrderByDescending(x => x.RES_Fecha)
                .Take(5)
                .Select(x => new DashboardRespuestaItemViewModel
                {
                    Codigo = x.RES_Codigo,
                    Encuesta = x.Encuesta.ENC_Nombre,
                    Fecha = x.RES_Fecha.ToString()
                })
                .ToList()
                .Select(x => new DashboardRespuestaItemViewModel
                {
                    Codigo = x.Codigo,
                    Encuesta = x.Encuesta,
                    Fecha = DateTime.Parse(x.Fecha).ToString("dd/MM/yyyy HH:mm")
                })
                .ToList();

            vm.EncuestasMasRespondidas = db.Respuestas
                .GroupBy(x => x.ENC_Encuesta)
                .Select(g => new DashboardEncuestaTopViewModel
                {
                    Encuesta = g.FirstOrDefault().Encuesta.ENC_Nombre,
                    TotalRespuestas = g.Count()
                })
                .OrderByDescending(x => x.TotalRespuestas)
                .Take(5)
                .ToList();

            return vm;
        }
    }
}