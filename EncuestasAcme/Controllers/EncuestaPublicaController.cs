using EncuestasAcme.DTOs.Encuesta;
using EncuestasAcme.Services;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Controllers
{
    [AllowAnonymous]
    public class EncuestaPublicaController : Controller
    {
        private readonly EncuestaService service;

        public EncuestaPublicaController()
        {
            service = new EncuestaService();
        }

        public ActionResult Responder(Guid id)
        {
            var encuesta = service.ObtenerPorToken(id);

            if (encuesta == null)
            {
                return HttpNotFound();
            }

            var vm = new ResponderEncuestaFormViewModel
            {
                Encuesta = encuesta,
                Respuesta = new ResponderEncuestaDTO
                {
                    ENC_Encuesta = encuesta.ENC_Encuesta,
                    Campos = encuesta.Campos.Select(c => new ResponderEncuestaCampoDTO
                    {
                        CAM_Campo = c.CAM_Campo,
                        TCA_Clave = c.TCA_Clave
                    }).ToList()
                }
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Responder(Guid id, ResponderEncuestaFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.Respuesta == null)
                {
                    return HttpNotFound();
                }

                var encuesta = service.ObtenerPorToken(id);

                if (encuesta == null)
                {
                    return HttpNotFound();
                }

                vm.Respuesta.ENC_Encuesta = encuesta.ENC_Encuesta;

                var respuestaService = new EncuestaRespuestaInternaService();

                respuestaService.GuardarRespuesta(
                    vm.Respuesta,
                    Request.UserHostAddress,
                    Request.UserAgent
                );

                return RedirectToAction("Gracias");
            }
            catch
            {
                var encuesta = service.ObtenerPorToken(id);

                if (vm == null)
                {
                    vm = new ResponderEncuestaFormViewModel();
                }

                vm.Encuesta = encuesta;
                return View(vm);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Gracias()
        {
            return View();
        }
    }
}